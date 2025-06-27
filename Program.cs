using NAudio.CoreAudioApi;
using System;
using System.Linq;

namespace SpeakerSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var deviceEnumerator = new MMDeviceEnumerator();
                
                // 获取所有启用的播放设备
                var playbackDevices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
                
                if (playbackDevices.Count < 2)
                {
                    // 系统中可用的音频输出设备少于2个，无法切换
                    return;
                }
                
                // 获取当前默认播放设备
                var currentDefaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                
                // 找到当前设备在列表中的索引
                int currentIndex = -1;
                for (int i = 0; i < playbackDevices.Count; i++)
                {
                    if (playbackDevices[i].ID == currentDefaultDevice.ID)
                    {
                        currentIndex = i;
                        break;
                    }
                }
                
                // 计算下一个设备的索引（循环切换）
                int nextIndex = (currentIndex + 1) % playbackDevices.Count;
                var nextDevice = playbackDevices[nextIndex];
                
                // 设置新的默认设备
                var policyConfig = new PolicyConfigClient();
                policyConfig.SetDefaultEndpoint(nextDevice.ID, Role.Multimedia);
                policyConfig.SetDefaultEndpoint(nextDevice.ID, Role.Communications);
                
                // 已切换到音频设备: {nextDevice.FriendlyName}
            }
            catch (Exception)
            {
                // 切换音频设备时发生错误
                Environment.Exit(1);
            }
        }
    }
    
    // PolicyConfig接口用于设置默认音频设备
    [System.Runtime.InteropServices.ComImport]
    [System.Runtime.InteropServices.Guid("f8679f50-850a-41cf-9c72-430f290290c8")]
    [System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
    interface IPolicyConfig
    {
        [System.Runtime.InteropServices.PreserveSig]
        int GetMixFormat(string pszDeviceName, IntPtr ppFormat);
        
        [System.Runtime.InteropServices.PreserveSig]
        int GetDeviceFormat(string pszDeviceName, bool bDefault, IntPtr ppFormat);
        
        [System.Runtime.InteropServices.PreserveSig]
        int ResetDeviceFormat(string pszDeviceName);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetDeviceFormat(string pszDeviceName, IntPtr pEndpointFormat, IntPtr MixFormat);
        
        [System.Runtime.InteropServices.PreserveSig]
        int GetProcessingPeriod(string pszDeviceName, bool bDefault, IntPtr pmftDefaultPeriod, IntPtr pmftMinimumPeriod);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetProcessingPeriod(string pszDeviceName, IntPtr pmftPeriod);
        
        [System.Runtime.InteropServices.PreserveSig]
        int GetShareMode(string pszDeviceName, IntPtr pMode);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetShareMode(string pszDeviceName, IntPtr mode);
        
        [System.Runtime.InteropServices.PreserveSig]
        int GetPropertyValue(string pszDeviceName, bool bFxStore, IntPtr key, IntPtr pv);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetPropertyValue(string pszDeviceName, bool bFxStore, IntPtr key, IntPtr pv);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetDefaultEndpoint(string pszDeviceName, Role role);
        
        [System.Runtime.InteropServices.PreserveSig]
        int SetEndpointVisibility(string pszDeviceName, bool bVisible);
    }
    
    [System.Runtime.InteropServices.ComImport]
    [System.Runtime.InteropServices.Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
    class PolicyConfigClient
    {
    }
    
    static class PolicyConfigClientExtensions
    {
        public static void SetDefaultEndpoint(this PolicyConfigClient client, string deviceId, Role role)
        {
            var policyConfig = (IPolicyConfig)client;
            policyConfig.SetDefaultEndpoint(deviceId, role);
        }
    }
}