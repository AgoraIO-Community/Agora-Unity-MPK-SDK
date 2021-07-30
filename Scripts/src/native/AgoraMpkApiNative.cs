//  AgoraMpkApiNative.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 27, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

using System;
using System.Runtime.InteropServices;

namespace agora.media_player
{
    using IrisMpkPtr = IntPtr;
    using IrisEventHandlerHandle = IntPtr;
    using IrisMpkRawDataPtr = IntPtr;
    using IrisMpkAudioFrameObserverHandle = IntPtr;
    using IrisMpkVideoFrameObserverHandle = IntPtr;
    using IrisVideoFrameBufferManagerPtr = IntPtr;
    using IrisVideoFrameBufferDelegateHandle = IntPtr;

    internal static class AgoraMpkNative
    {
        #region DllImport

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        private const string AgoraMpkLibName = "AgoraMpkWrapper";
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        private const string AgoraMpkLibName = "AgoraMpkWrapperUnity";
#elif UNITY_IPHONE
		private const string AgoraMpkLibName = "__Internal";
#else
        private const string AgoraMpkLibName = "AgoraMpkWrapper";
#endif
        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisMpkPtr CreateIrisMpk();

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DestroyIrisMpk(IrisMpkPtr mpk_ptr);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisEventHandlerHandle SetIrisMpkEventHandler(IrisMpkPtr mpk_ptr, IntPtr event_handler);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UnsetIrisMpkEventHandler(IrisMpkPtr mpk_ptr, IrisEventHandlerHandle handle);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CallIrisMpkApi(IrisMpkPtr mpk_ptr, ApiTypeMpk api_type, string @params,
            out CharAssistant result);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisMpkRawDataPtr GetIrisMpkRawData(IrisMpkPtr mpk_ptr);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisMpkAudioFrameObserverHandle RegisterAudioFrameObserver(
            IrisMpkRawDataPtr raw_data_ptr, IntPtr observerNative, int order, string identifier);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UnRegisterAudioFrameObserver(IrisMpkRawDataPtr raw_data_ptr,
            IrisMpkAudioFrameObserverHandle handle, string identifier);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisMpkVideoFrameObserverHandle RegisterVideoFrameObserver(
            IrisMpkRawDataPtr raw_data_ptr, IntPtr observerNative, int order, string identifier);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UnRegisterVideoFrameObserver(IrisMpkRawDataPtr raw_data_ptr,
            IrisMpkVideoFrameObserverHandle handle, string identifier);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisVideoFrameBufferManagerPtr CreateIrisVideoFrameBufferManager();

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FreeIrisVideoFrameBufferManager(IrisVideoFrameBufferManagerPtr manager_ptr);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisVideoFrameBufferDelegateHandle EnableVideoFrameBuffer(
            IrisVideoFrameBufferManagerPtr manager_ptr, ref IrisMpkCVideoFrameBufferNative buffer, uint uid,
            string channel_id = "");

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GetVideoFrame(IrisVideoFrameBufferManagerPtr manager_ptr,
            ref IrisMpkVideoFrame video_frame, out bool is_new_frame, uint uid, string channel_id = "");

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DisableVideoFrameBufferByUid(IrisVideoFrameBufferManagerPtr manager_ptr, uint uid,
            string channel_id = "");

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attach(IrisMpkRawDataPtr raw_data_ptr, IrisVideoFrameBufferManagerPtr manager_ptr);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Detach(IrisMpkRawDataPtr raw_data_ptr, IrisVideoFrameBufferManagerPtr manager_ptr);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IrisMpkVideoFrame ConvertVideoFrame(ref IrisMpkVideoFrame src, VIDEO_FRAME_TYPE format);

        [DllImport(AgoraMpkLibName, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ClearVideoFrame(ref IrisMpkVideoFrame video_frame);

        #endregion
    }
}