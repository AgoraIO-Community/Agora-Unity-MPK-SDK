//  Base.cs
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
    internal enum ApiTypeMpk
    {
        kMpkInitialize,
        kMpkRelease,
        kMpkGetPlayerSdkVersion,
        kMpkOpen,
        kMpkPlay,
        kMpkPause,
        kMpkStop,
        kMpkSeek,
        kMpkMute,
        kMpkGetMute,
        kMpkAdjustPlayoutVolume,
        kMpkGetPlayoutVolume,
        kMpkGetDuration,
        kMpkGetPlayPosition,
        kMpkGetStreamCount,
        kMpkGetStreamInfo,
        kMpkSetView,
        kMpkSetRenderMode,
        kMpkConnect,
        kMpkDisconnect,
        kMpkPublishVideo,
        kMpkUnpublishVideo,
        kMpkPublishAudio,
        kMpkUnpublishAudio,
        kMpkAdjustPublishSignalVolume,
        kMpkSetLogFile,
        kMpkSetLogFilter,
        kMpkChangePlaybackSpeed,
        kMpkSelectAudioTrack,
        kMpkSetPlayerOption,
        kMpkTakeScreenshot,
        kMpkSelectInternalSubtitle,
        kMpkSetExternalSubtitle,
        kMpkSetLoopCount,
    }

    internal enum AudioFrameType
    {
        kAudioFrameTypePCM16,
    }

    internal enum VideoFrameType
    {
        kVideoFrameTypeYUV420,
        kVideoFrameTypeYUV422,
        kVideoFrameTypeRGBA,
        kVideoFrameTypeBGRA,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisMpkAudioFrame
    {
        internal AUDIO_FRAME_TYPE type;
        internal int samples;
        internal int bytes_per_sample;
        internal int channels;
        internal int samples_per_sec;
        internal IntPtr buffer;
        internal uint buffer_length;
        internal long render_time_ms;
        internal int av_sync_type;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IrisMpkVideoFrame
    {
        internal VIDEO_FRAME_TYPE type;
        internal int width;
        internal int height;
        internal int y_stride;
        internal int u_stride;
        internal int v_stride;
        internal IntPtr y_buffer;
        internal IntPtr u_buffer;
        internal IntPtr v_buffer;
        internal uint y_buffer_length;
        internal uint u_buffer_length;
        internal uint v_buffer_length;
        internal int rotation;
        internal long render_time_ms;
        internal int av_sync_type;
    }
}