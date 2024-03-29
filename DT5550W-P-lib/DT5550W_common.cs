﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DT5550W_P_lib
{
    public enum tPOLARITY
    {
        POSITIVE = 0,
        NEGATIVE = 1
    }
    public enum TriggerMode { TIME_TRIG, CHARGE_TRIG, EXT_TRIG, GBL_TRIG_TIME, GBL_TRIG_CHARGE, SELF_TRIG, TWO_COINC_TIME, GBL_TWO_COINC_TIME };

    public enum T0Mode { SOFTWARE_STARTRUN = 0, SOFTWARE_REG = 1, SOFTWARE_PERIODIC = 2, EXTERNAL = 3, OFF = 4 };
    public enum PCMode { PERIODIC_WIN = 0, EXTERNAL_WIN = 1, PERIODIC_WIN_EXT_START =2, PERIODIC_WIN_INT_START =3};

    public enum RunStartMode { INTERNAL_START = 0, RESET_FROM_EXT = 1, LATCHED = 2 };

    public enum t_AsicModels
    {
        PETIROC = 0,
        MAROC = 1,
        CITIROC = 2,
        GEMROC = 3,
        PHOTOROC = 4,
        INVALID = 99
    }

    
    public struct tChannelMapping
    {
        public int X;
        public int Y;
        public int ASICID;
    }


    public class t_DataCITIROC
    {
        public UInt64 EventTimecode;
        public UInt64 RunEventTimecode;
        public UInt64 EventCounter;
        public UInt16 AsicID;
        public UInt32 TriggerID;
        public UInt32 ValidationID;
        public UInt32 Flags;

        public double EventTimecode_ns;
        public double RunEventTimecode_ns;

        public ushort[] chargeHG;
        public ushort[] chargeLG;
        public bool[] hit;
        public t_DataCITIROC()
        {
            chargeLG = new ushort[32];
            chargeHG = new ushort[32];
            hit = new bool[32];
        }

    }

    public class t_DataCITIROCPC
    {
        public UInt64 EventTimecode;
        public UInt64 PacketID;
        public UInt64 WindowsID;
        public UInt64 StartID;

        public double EventTimecode_ns;
        

        public UInt64[] counters;
        public t_DataCITIROCPC()
        {
            counters = new UInt64[128];
        }

    }


    public struct t_BoardInfo
        {
            public enum t_AsicType { PETIROC = 0, MAROC = 1, CITIROC = 2, INVALID=99 };
            public t_AsicType AsicType;
            public int totalAsics;
            public int channelsPerAsic;
            public int totalChannels;
            public double FPGATimecode_ns;
            public double Coarse_ns;
            public double Fine_ns;
            public string SerialNumber;
            public int DigitalDataPacketSize;
            public enum t_DefaultDetectorLayout { MATRIX_4x4 = 0, MATRIX_8x8 = 1, LINEAR = 2 };
            public t_DefaultDetectorLayout DefaultDetectorLayout;
        }


    public class PHY_LINK
    {
        bool connected = false;
        private Mutex mut = new Mutex();

        public enum USB_BUS_MODE
        {
            REG_ACCESS = 0,
            STREAMING = 1
        };


        IntPtr Handle;

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_Init(
        );

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_ListDevices(
            StringBuilder ListOfDevice,
            String model,
            ref int Count
        );


        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_ConnectDevice(
                String serial_number,
                IntPtr handle);


        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_AllocHandle(
        ref IntPtr handle);

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_CloseConnection(
        IntPtr handle);


        public PHY_LINK()
        {
            Handle = new IntPtr();
            NI_USB3_AllocHandle(ref Handle);
            NI_USB3_Init();
        }

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_WriteData(
                UInt32[] data,
                UInt32 count,
                UInt32 address,
                USB_BUS_MODE bus_mode,
                UInt32 timeout_ms,
                IntPtr handle,
                ref IntPtr written_data);

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_ReadData(
                UInt32[] data,
                UInt32 count,
                UInt32 address,
                USB_BUS_MODE bus_mode,
                UInt32 timeout_ms,
                IntPtr handle,
                ref IntPtr read_data,
                ref IntPtr valid_data
                );

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_WriteReg(
                UInt32 data,
                UInt32 address,
                IntPtr handle
                );

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_ReadReg(
                ref UInt32 data,
                UInt32 address,
                IntPtr handle
                );

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_IIC_WriteData(
                Byte address,
                Byte[] value,
                int len, IntPtr handle);

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_IIC_ReadData(
        Byte address,
        Byte[] value,
        int len,
        Byte[] value_read,
        int len_read,
        IntPtr handle);

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_SetIICControllerBaseAddress(
            UInt32 ControlAddress,
            UInt32 StatusAddress,
            IntPtr handle);


        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_SetHV(
                   bool Enable,
                   float voltage,
                   IntPtr handle);

        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_GetHV(
            ref bool Enable,
            ref float voltage,
            ref float current,
            IntPtr handle);


        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_GetDT5550_DGBoardInfo(
            StringBuilder model,
            ref int asic_count,
            ref int SN,
            IntPtr handle);


        [DllImport("niusb3_core.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe private static extern int NI_USB3_GetDT5550WTempSens(
            int address,
            ref float temperature,
            IntPtr handle);





        public int NI_USB3_WriteData_M(
                UInt32[] data,
                UInt32 count,
                UInt32 address,
                USB_BUS_MODE bus_mode,
                UInt32 timeout_ms,
                ref UInt32 written_data)
        {
            //IntPtr unmanagedPointer = Marshal.AllocHGlobal((Int32) count);
            if (!connected) return -100000;
            IntPtr written_data_ptr = new IntPtr(written_data);
            mut.WaitOne();
            int retcode = NI_USB3_WriteData(
               data,
               count,
               address,
               bus_mode,
               timeout_ms,
               Handle,
               ref written_data_ptr);

            written_data = (UInt32)written_data_ptr.ToInt32();
            mut.ReleaseMutex();
            return retcode;
        }

        public int NI_USB3_ReadData_M(
                UInt32[] data,
                UInt32 count,
                UInt32 address,
                USB_BUS_MODE bus_mode,
                UInt32 timeout_ms,
                ref UInt32 read_data,
                ref UInt32 valid_data
                )
        {
            
            IntPtr read_data_ptr = new IntPtr(1);
            IntPtr valid_data_ptr = new IntPtr(1);
            if (!connected)
            {
                read_data = 0;
                valid_data = 0;
                return -100000;
            }
            mut.WaitOne();
            int retcode = NI_USB3_ReadData(
                    data,
                    count,
                    address,
                    bus_mode,
                    timeout_ms,
                    Handle,
                    ref read_data_ptr,
                    ref valid_data_ptr
                    );
            read_data = (UInt32)read_data_ptr.ToInt32();
            valid_data = (UInt32)valid_data_ptr.ToInt32();
            mut.ReleaseMutex();
            //Console.WriteLine("R-MEM[" + address.ToString() + "]: " + count.ToString() + " - " + valid_data);
            return retcode;
        }


        public int NI_USB3_WriteReg_M(UInt32 data,
                UInt32 address)
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res = NI_USB3_WriteReg(data, address, Handle);
            mut.ReleaseMutex();
            //Console.WriteLine("W-REG[" + address.ToString() + "]: " + data.ToString() );
            return res;
        }

        public int NI_USB3_ReadReg_M(ref UInt32 data,
          UInt32 address)
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res = NI_USB3_ReadReg(ref data, address, Handle);
            //Console.WriteLine("R-REG[" + address.ToString() + "]: " + data.ToString());
            mut.ReleaseMutex();
            return res;
        }



        unsafe public int IIC_WriteData(byte address, byte[] value, int len)
        {
            if (!connected) return -100000;
            mut.WaitOne();

            if (NI_USB3_IIC_WriteData(
                address,
                value,
                len, Handle) != 0)
            {
                mut.ReleaseMutex();
                return -1;

            }

            mut.ReleaseMutex();
            return 0;
        }



        public List<String> GetDeviceList()
        {


            StringBuilder sb = new StringBuilder(2048);
            int count = 0;
            int retcode = NI_USB3_ListDevices(
            sb,
            null,
            ref count);

            if (retcode != 0)
                return null;

            List<String> devs = new List<string>();

            foreach (String a in sb.ToString().Split(';'))
            {
                if (a.Length > 1)
                    devs.Add(a);
            }

            return devs;
        }

        public int NI_USB3_ConnectDevice_M(String serial_number)
        {
            if (connected) return -100000;
            int ret =  NI_USB3_ConnectDevice(
                  serial_number,
                   Handle);
            uint data = 0;
            if (ret == 0) connected = true;
            /*
            NI_USB3_WriteReg_M(30 * 10000, 0xFFFF0002);
            NI_USB3_WriteReg_M(0, 0xFFFF0000);

            while(true)
            {
                NI_USB3_ReadReg_M(ref data, 0xFFFF0010);
                Console.WriteLine("Status: " + data.ToString());
                NI_USB3_ReadReg_M(ref data, 0xFFFF0011);
                Console.WriteLine("voltage: " + (((double)data)/10000).ToString());
            }*/


            return ret;
        }
        public void NI_USB3_Disconnect_M()
        {
            if (!connected) return;

            mut.WaitOne();
            connected = false;
            NI_USB3_CloseConnection(Handle);
            mut.ReleaseMutex();
        }

        public int NI_USB3_SetIICControllerBaseAddress_M(UInt32 ControlAddress,
            UInt32 StatusAddress)
        {
            if (!connected) return -100000;
            return NI_USB3_SetIICControllerBaseAddress(
            ControlAddress,
            StatusAddress,
            Handle);
        }

        public int NI_USB3_SetHV_M(
           bool Enable,
           float voltage)
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res;
            res = NI_USB3_SetHV(
                               Enable,
                               voltage,
                               Handle);
            mut.ReleaseMutex();
            return res;
        }


        public int NI_USB3_GetHV_M(
                 ref bool Enable,
                 ref float voltage,
                 ref float current
                 )
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res = NI_USB3_GetHV(
                ref Enable,
                ref voltage,
                ref current,
                Handle);

            mut.ReleaseMutex();
            return res;
        }


        public int NI_USB3_GetDT5550WTempSens_M(
               int address,
               ref float temperature)
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res = NI_USB3_GetDT5550WTempSens(
                address,
                ref temperature,
                Handle);
            mut.ReleaseMutex();
            return res;
        }

        public int NI_USB3_GetDT5550_DGBoardInfo_M(
                StringBuilder model,
                ref int asic_count,
                ref int SN)
        {
            if (!connected) return -100000;
            mut.WaitOne();
            int res = NI_USB3_GetDT5550_DGBoardInfo(
                 model,
                ref asic_count,
                ref SN,
                Handle);

            mut.ReleaseMutex();
            return res;
        }



    }



    public class CitirocMonitorData
    {
        public float[] A_chargeHG;
        public float[] A_chargeLG;
        public float[] A_HIT;
        public float[] A_CLK;
        public float[] A_SR;
        public float[] A_TrigT;
        public float[] A_TrigC;
        public float[] A_Trig;
        public float[] A_Trig_Ext;
        public float[] A_Veto_Ext;

        public float[] B_chargeHG;
        public float[] B_chargeLG;
        public float[] B_HIT;
        public float[] B_CLK;
        public float[] B_SR;
        public float[] B_TrigT;
        public float[] B_TrigC;
        public float[] B_Trig;
        public float[] B_Trig_Ext;
        public float[] B_Veto_Ext;


        public float[] C_chargeHG;
        public float[] C_chargeLG;
        public float[] C_HIT;
        public float[] C_CLK;
        public float[] C_SR;
        public float[] C_TrigT;
        public float[] C_TrigC;
        public float[] C_Trig;
        public float[] C_Trig_Ext;
        public float[] C_Veto_Ext;

        public float[] D_chargeHG;
        public float[] D_chargeLG;
        public float[] D_HIT;
        public float[] D_CLK;
        public float[] D_SR;
        public float[] D_TrigT;
        public float[] D_TrigC;
        public float[] D_Trig;
        public float[] D_Trig_Ext;
        public float[] D_Veto_Ext;


        public CitirocMonitorData(int size)
        {
            A_chargeHG = new float[size];
            A_chargeLG = new float[size];
            A_HIT = new float[size];
            A_CLK = new float[size];
            A_SR = new float[size];
            A_TrigT = new float[size];
            A_TrigC = new float[size];
            A_Trig = new float[size];
            A_Trig_Ext = new float[size];
            A_Veto_Ext = new float[size];

            B_chargeHG = new float[size];
            B_chargeLG = new float[size];
            B_HIT = new float[size];
            B_SR = new float[size];
            B_CLK = new float[size];
            B_TrigT = new float[size];
            B_TrigC = new float[size];
            B_Trig = new float[size];
            B_Trig_Ext = new float[size];
            B_Veto_Ext = new float[size];

            C_chargeHG = new float[size];
            C_chargeLG = new float[size];
            C_HIT = new float[size];
            C_SR = new float[size];
            C_CLK = new float[size];
            C_TrigT = new float[size];
            C_TrigC = new float[size];
            C_Trig = new float[size];
            C_Trig_Ext = new float[size];
            C_Veto_Ext = new float[size];

            D_chargeHG = new float[size];
            D_chargeLG = new float[size];
            D_HIT = new float[size];
            D_SR = new float[size];
            D_CLK = new float[size];
            D_TrigT = new float[size];
            D_TrigC = new float[size];
            D_Trig = new float[size];
            D_Trig_Ext = new float[size];
            D_Veto_Ext = new float[size];

        }

    }
}
