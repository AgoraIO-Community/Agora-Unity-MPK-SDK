//  AgoraCallback.cs
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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal delegate void Func_Event_Native(string @event, string data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal delegate void Func_EventWithBuffer_Native(string @event, string data, IntPtr buffer, uint length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal delegate bool Func_AudioFrameLocal_Native(IntPtr audio_frame);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal delegate bool Func_VideoFrameLocal_Native(IntPtr video_frame);

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisCEventHandlerNative
    {
        internal IntPtr onEvent;
        internal IntPtr onEventWithBuffer;
    }

    internal struct IrisCEventHandler
    {
        internal Func_Event_Native OnEvent;
        internal Func_EventWithBuffer_Native OnEventWithBuffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisMpkCAudioFrameObserverNative
    {
        internal IntPtr OnAudioFrame;
    }

    internal struct IrisMpkCAudioFrameObserver
    {
        internal Func_AudioFrameLocal_Native OnAudioFrame;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisMpkCVideoFrameObserverNative
    {
        internal IntPtr OnVideoFrame;
    }

    internal struct IrisMpkCVideoFrameObserver
    {
        internal Func_AudioFrameLocal_Native OnVideoFrame;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisMpkCVideoFrameBufferNative
    {
        internal int type;
        internal IntPtr OnVideoFrameReceived;
        internal int resize_width;
        internal int resize_height;
    }
}