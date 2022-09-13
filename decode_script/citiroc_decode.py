import sys, os
import numpy as np
import pandas as pd


def read_citiroc_binfile(filename, verbose=False):
    if verbose:
        print("Reading file ", filename)
    nword_per_event = 9+32
    
    dt = np.dtype('u4')
    data = np.fromfile(filename,dtype=dt)
    idx_hdr = np.where(data>>4==0x8000000)[0] ## index of headers
    #idx_foo = np.where(data== 0xc0000000)[0] ## index of footers
    nevts = len(idx_hdr)
    evt_lengths = np.diff(idx_hdr) ## lunghezza di ciascun evento in "word"
    flag_bad=True
    
    evidx_bad = np.where(evt_lengths!=nword_per_event)[0] #indice degli eventi bad
    if len(evidx_bad)==0:
        flag_bad=False
        evidx_bad = [-1]
    nevts_bad = len(evidx_bad)
    if verbose:
        print("Number of events = ",nevts)
        if flag_bad:
            print("Number of corrupted events = ",nevts_bad)
            print("Fraction of corrupted events = ",nevts_bad/nevts*100,"%")
    
    dt = np.dtype([('head','u4'),\
                   ('EventTimecode','u4'),\
                   ('RunEventTimecode','u8'),\
                   ('EventCounter','u4'),\
                   ('body','i4',(32,)),\
                   ('TriggerID','u4'),\
                   ('ValidationID','u4'),\
                   ('flag','u4'),\
                   ('footer','u4')])

    
    ## read the first chunk
    first_word=idx_hdr[0]
    count=evidx_bad[0]
    all_data_decoded = np.fromfile(filename,dtype=dt,offset=first_word*4, count=count)
    ## read the core chunk
    for i in range(nevts_bad-1):
        first_word = idx_hdr[evidx_bad[i]+1]
        count = evidx_bad[i+1]- evidx_bad[i]-1
        data_decoded1 = np.fromfile(filename,dtype=dt,offset=first_word*4, count=count)
        all_data_decoded = np.append(all_data_decoded, data_decoded1)

    if flag_bad:
        ## read the last chunk
        first_word = idx_hdr[evidx_bad[-1]+1]
        count=-1
        data_decoded1 = np.fromfile(filename,dtype=dt,offset=first_word*4, count=count)
        all_data_decoded = np.append(all_data_decoded, data_decoded1)


    events = {'AsicID': all_data_decoded["head"] & 0xF ,
          'EventTimecode': all_data_decoded['EventTimecode'],
          'EventTimecode_ns':  all_data_decoded['EventTimecode']*.5,
          'RunEventTimecode': all_data_decoded['RunEventTimecode'],
          'RunEventTimecode_ns': all_data_decoded['RunEventTimecode']*.5,
          'EventCounter': all_data_decoded['EventCounter'],
          'chargeHG': (all_data_decoded['body']>>0 & 0x3fff)[:,::-1],
          'chargeLG': (all_data_decoded['body']>>14 & 0x3fff)[:,::-1],
          'hit': (all_data_decoded['body']>>28 & 0x1)[:,::-1],
          'TriggerID': all_data_decoded['TriggerID'],
          'ValidationID': all_data_decoded['ValidationID'],
          'fake': all_data_decoded['flag']}
    header = all_data_decoded['head']
    footer = all_data_decoded["footer"] 
    check1 = header>>4 == 0x8000000 # corrupted events are False
    check2 = footer == 0xc0000000 # good events are True
    mask = np.array([a & b for a, b in zip(check1,check2)])

    for key in events.keys():
        events[key] = events[key][mask]
    if verbose:
        print("Number of survived events: ", mask.sum())#, "/", len(mask))
    
    return events
#    return events, all_data_decoded['head'], all_data_decoded['footer']

events = read_citiroc_binfile("c:\\temp\\4.data", verbose=True)
nasic0=0
nasic1=1
nasic2=2
nasic3=3
for e in events["AsicID"]:
    if e == 0:
        nasic0+=1
    if e == 1:
        nasic1+=1
    if e == 2:
        nasic2+=1
    if e == 3:
        nasic3+=1

print("Number of events in file:")
print(" Asic 0:   " + str(nasic0))
print(" Asic 1:   " + str(nasic1))
print(" Asic 2:   " + str(nasic2))
print(" Asic 3:   " + str(nasic3))
