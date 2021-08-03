//  VideoRender.cs
//
//  Created by Yiqing Huang on July 29, 2021.
//  Modified by Yiqing Huang on July 29, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

using System;

namespace agora.media_player
{
    using IrisRtcRendererCacheConfigHandle = IntPtr;

    internal abstract class IVideoStreamManager
    {
        internal abstract int EnableVideoFrameCache(int width, int height, uint uid, string channel_id = "");

        internal abstract void DisableVideoFrameCache(uint uid = 0, string channel_id = "");

        internal abstract bool GetVideoFrame(ref IrisMpkVideoFrame video_frame,
            ref bool is_new_frame, uint uid, string channel_id = "");
    }

    internal class VideoStreamManager : IVideoStreamManager, IDisposable
    {
        private IAgoraMediaPlayer _agoraMediaPlayer;
        private IrisRtcRendererCacheConfigHandle _irisVideoFrameBufferDelegateHandle;
        private IrisMpkCVideoFrameBufferNative _videoFrameBuffer;
        private bool _disposed;

        public VideoStreamManager(IAgoraMediaPlayer agoraMediaPlayer)
        {
            this._agoraMediaPlayer = agoraMediaPlayer;
        }

        ~VideoStreamManager()
        {
            Dispose();
        }

        internal override int EnableVideoFrameCache(int width, int height, uint uid, string channel_id = "")
        {
            if (_agoraMediaPlayer == null)
            {
                AgoraLog.LogError(string.Format("EnableVideoFrameCache ret: ${0}",
                    MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED));
                return (int) MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED;
            }

            IntPtr irisMediaPlayer = (_agoraMediaPlayer as AgoraMediaPlayer).GetNativeHandler();

            if (irisMediaPlayer != IntPtr.Zero)
            {
                var managerPtr = (_agoraMediaPlayer as AgoraMediaPlayer).GetIrisMpkVideoFrameBufferManagerPtr();
                _videoFrameBuffer = new IrisMpkCVideoFrameBufferNative
                {
                    type = (int) VIDEO_FRAME_TYPE.FRAME_TYPE_RGBA,
                    OnVideoFrameReceived = IntPtr.Zero,
                    resize_width = width,
                    resize_height = height
                };
                _irisVideoFrameBufferDelegateHandle =
                    AgoraMpkNative.EnableVideoFrameBuffer(managerPtr, ref _videoFrameBuffer, uid, channel_id);
                return (int) MEDIA_PLAYER_ERROR.PLAYER_ERROR_NONE;
            }

            return (int) MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED;
        }

        internal override void DisableVideoFrameCache(uint uid = 0, string channel_id = "")
        {
            if (_agoraMediaPlayer == null)
            {
                AgoraLog.LogError(string.Format("EnableVideoFrameCache ret: ${0}",
                    MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED));
                return;
            }

            IntPtr irisMediaPlayer = (_agoraMediaPlayer as AgoraMediaPlayer).GetNativeHandler();

            if (irisMediaPlayer != IntPtr.Zero)
            {
                var managerPtr = (_agoraMediaPlayer as AgoraMediaPlayer).GetIrisMpkVideoFrameBufferManagerPtr();

                AgoraMpkNative.DisableVideoFrameBufferByUid(managerPtr, uid, channel_id);
            }
        }

        internal override bool GetVideoFrame(ref IrisMpkVideoFrame video_frame,
            ref bool is_new_frame, uint uid, string channel_id = "")
        {
            if (_agoraMediaPlayer == null)
            {
                AgoraLog.LogError(string.Format("EnableVideoFrameCache ret: ${0}",
                    MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED));
                return false;
            }

            IntPtr irisMediaPlayer = (_agoraMediaPlayer as AgoraMediaPlayer).GetNativeHandler();

            if (irisMediaPlayer != IntPtr.Zero)
            {
                var managerPtr = (_agoraMediaPlayer as AgoraMediaPlayer).GetIrisMpkVideoFrameBufferManagerPtr();

                return AgoraMpkNative.GetVideoFrame(managerPtr, ref video_frame, out is_new_frame, uid, channel_id);
            }

            return false;
        }

        internal void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _agoraMediaPlayer = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}