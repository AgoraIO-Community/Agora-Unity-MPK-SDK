//  AgoraMpkVideoFrameObserver.cs
//
//  Created by Yiqing Huang on July 28, 2021.
//  Modified by Yiqing Huang on July 28, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

using System;
using System.Runtime.InteropServices;
using AOT;

namespace agora.media_player
{
    public class MpkVideoFrameObserverNative
    {
        internal static IAgoraMpkVideoFrameObserver VideoFrameObserver;
        private static readonly VideoFrame CaptureVideoFrame = new VideoFrame();

        [MonoPInvokeCallback(typeof(Func_VideoFrameLocal_Native))]
        internal static bool OnCaptureVideoFrame(IntPtr videoFramePtr)
        {
            if (VideoFrameObserver == null) return true;

            var videoFrame = (IrisMpkVideoFrame) (Marshal.PtrToStructure(videoFramePtr, typeof(IrisMpkVideoFrame)) ??
                                                        new IrisMpkVideoFrame());
            
            var ifConverted = VideoFrameObserver.GetVideoFormatPreference() != VIDEO_FRAME_TYPE.FRAME_TYPE_YUV420;
            var videoFrameConverted = ifConverted
                ? AgoraMpkNative.ConvertVideoFrame(ref videoFrame, VideoFrameObserver.GetVideoFormatPreference())
                : videoFrame;

            if (CaptureVideoFrame.height != videoFrameConverted.height ||
                CaptureVideoFrame.yStride != videoFrameConverted.y_stride ||
                CaptureVideoFrame.uStride != videoFrameConverted.u_stride ||
                CaptureVideoFrame.vStride != videoFrameConverted.v_stride)
            {
                CaptureVideoFrame.yBuffer = new byte[videoFrameConverted.y_buffer_length];
                CaptureVideoFrame.uBuffer = new byte[videoFrameConverted.u_buffer_length];
                CaptureVideoFrame.vBuffer = new byte[videoFrameConverted.v_buffer_length];
            }

            if (videoFrameConverted.y_buffer != IntPtr.Zero)
                Marshal.Copy(videoFrameConverted.y_buffer, CaptureVideoFrame.yBuffer, 0,
                    (int) videoFrameConverted.y_buffer_length);
            if (videoFrameConverted.u_buffer != IntPtr.Zero)
                Marshal.Copy(videoFrameConverted.u_buffer, CaptureVideoFrame.uBuffer, 0,
                    (int) videoFrameConverted.u_buffer_length);
            if (videoFrameConverted.v_buffer != IntPtr.Zero)
                Marshal.Copy(videoFrameConverted.v_buffer, CaptureVideoFrame.vBuffer, 0,
                    (int) videoFrameConverted.v_buffer_length);
            CaptureVideoFrame.width = videoFrameConverted.width;
            CaptureVideoFrame.height = videoFrameConverted.height;
            CaptureVideoFrame.yBufferPtr = videoFrameConverted.y_buffer;
            CaptureVideoFrame.yStride = videoFrameConverted.y_stride;
            CaptureVideoFrame.uBufferPtr = videoFrameConverted.u_buffer;
            CaptureVideoFrame.uStride = videoFrameConverted.u_stride;
            CaptureVideoFrame.vBufferPtr = videoFrameConverted.v_buffer;
            CaptureVideoFrame.vStride = videoFrameConverted.v_stride;
            CaptureVideoFrame.rotation = videoFrameConverted.rotation;
            CaptureVideoFrame.renderTimeMs = videoFrameConverted.render_time_ms;
            CaptureVideoFrame.avsync_type = videoFrameConverted.av_sync_type;

            if (ifConverted) AgoraMpkNative.ClearVideoFrame(ref videoFrameConverted);

            return VideoFrameObserver.OnCaptureVideoFrame(CaptureVideoFrame);
        }
    }
}