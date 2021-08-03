//  AgoraMediaPlayer.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 28, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

using System;
using System.Runtime.InteropServices;
using AOT;

namespace agora.media_player
{
    using LitJson;
    using view_t = UInt64;
    using IrisMpkPtr = IntPtr;
    using IrisEventHandlerHandleNative = IntPtr;
    using IrisVideoFrameBufferManagerPtr = IntPtr;

    public class AgoraMediaPlayer : IAgoraMediaPlayer
    {
        private bool _disposed;

        private static AgoraMediaPlayer _mpkInstance;
        private IrisMpkPtr _irisMpkPtr;

        private IrisEventHandlerHandleNative _irisEventHandlerHandleNative;
        private IrisCEventHandler _irisCEventHandler;
        private IrisEventHandlerHandleNative _irisCEventHandlerNative;
        private AgoraCallbackObject _callbackObject;

        private IrisVideoFrameBufferManagerPtr _irisVideoFrameBufferManagerPtr;

        private CharAssistant _result;

        private AgoraMediaPlayer()
        {
            _result = new CharAssistant();
            _irisMpkPtr = AgoraMpkNative.CreateIrisMpk();

            _irisVideoFrameBufferManagerPtr = AgoraMpkNative.CreateIrisVideoFrameBufferManager();
            AgoraMpkNative.Attach(AgoraMpkNative.GetIrisMpkRawData(_irisMpkPtr), _irisVideoFrameBufferManagerPtr);
        }

        public static IAgoraMediaPlayer CreateAgoraMediaPlayer()
        {
            return _mpkInstance ?? (_mpkInstance = new AgoraMediaPlayer());
        }

        public override void Dispose(bool sync = true)
        {
            Dispose(true, sync);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing, bool sync)
        {
            if (_disposed) return;

            if (disposing)
            {
                AgoraMpkNative.Detach(AgoraMpkNative.GetIrisMpkRawData(_irisMpkPtr), _irisVideoFrameBufferManagerPtr);
                // AgoraMpkNative.FreeIrisVideoFrameBufferManager(_irisVideoFrameBufferManagerPtr); // Currently, iris free this buffer when calling Detach function.

                UnregisterPlayerObserver();
            }

            Release(sync);

            _disposed = true;
        }

        private void Release(bool sync = true)
        {
            var param = new
            {
                sync
            };
            AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkRelease, JsonMapper.ToJson(param), out _result);
            AgoraMpkNative.DestroyIrisMpk(_irisMpkPtr);
            _irisMpkPtr = IntPtr.Zero;
            _result = new CharAssistant();
            _mpkInstance = null;
        }

        public override int Initialize(MediaPlayerContext context)
        {
            var param = new
            {
                context
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkInitialize, JsonMapper.ToJson(param),
                out _result);
        }

        public static IAgoraMediaPlayer Get()
        {
            return _mpkInstance;
        }

        internal IVideoStreamManager GetVideoStreamManager()
        {
            return new VideoStreamManager(this);
        }

        internal IrisMpkPtr GetNativeHandler()
        {
            return _irisMpkPtr;
        }

        internal IrisVideoFrameBufferManagerPtr GetIrisMpkVideoFrameBufferManagerPtr()
        {
            return _irisVideoFrameBufferManagerPtr;
        }

        public override int Open(string src, long startPos = 0)
        {
            var param = new
            {
                src,
                startPos
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkOpen, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Play()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkPlay, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Pause()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkPause, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Stop()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkStop, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Seek(long pos)
        {
            var param = new
            {
                pos
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSeek, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Mute(bool mute = false)
        {
            var param = new
            {
                mute
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkMute, JsonMapper.ToJson(param),
                out _result);
        }

        public override bool GetMute()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetMute, JsonMapper.ToJson(param),
                out _result);
            if (res < 0) return false;
            return JsonMapper.ToObject<bool>(_result.Result);
        }

        public override int AdjustPlayoutVolume(int volume)
        {
            var param = new
            {
                volume
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkAdjustPlayoutVolume,
                JsonMapper.ToJson(param), out _result);
        }

        public override int GetPlayoutVolume()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetPlayoutVolume,
                JsonMapper.ToJson(param), out _result);
            if (res < 0) return res;
            return JsonMapper.ToObject<int>(_result.Result);
        }

        public override long GetDuration()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetDuration, JsonMapper.ToJson(param),
                out _result);
            if (res < 0) return res;
            return JsonMapper.ToObject<long>(_result.Result);
        }

        public override long GetPlayPosition()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetPlayPosition,
                JsonMapper.ToJson(param), out _result);
            if (res < 0) return res;
            return JsonMapper.ToObject<long>(_result.Result);
        }

        public override long GetStreamCount()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetStreamCount,
                JsonMapper.ToJson(param), out _result);
            if (res < 0) return res;
            return JsonMapper.ToObject<long>(_result.Result);
        }

        public override int GetStreamInfo(long index, out MediaStreamInfo info)
        {
            var param = new
            {
                index
            };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetStreamInfo, JsonMapper.ToJson(param),
                out _result);
            info = _result.Result.Length == 0
                ? new MediaStreamInfo()
                : AgoraJson.JsonToStruct<MediaStreamInfo>(_result.Result);
            return res;
        }

        public override int SetView(ulong view)
        {
            var param = new
            {
                view
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetView, JsonMapper.ToJson(param),
                out _result);
        }

        public override int SetRenderMode(RENDER_MODE_TYPE renderMode)
        {
            var param = new
            {
                renderMode
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetRenderMode, JsonMapper.ToJson(param),
                out _result);
        }

        public override int RegisterPlayerObserver(IAgoraMediaPlayerObserver observer)
        {
            if (_irisEventHandlerHandleNative == IntPtr.Zero)
            {
                _irisCEventHandler = new IrisCEventHandler
                {
                    OnEvent = MediaPlayerObserverNative.OnEvent,
                    OnEventWithBuffer = MediaPlayerObserverNative.OnEventWithBuffer
                };

                var cEventHandlerNativeLocal = new IrisCEventHandlerNative
                {
                    onEvent = Marshal.GetFunctionPointerForDelegate(_irisCEventHandler.OnEvent),
                    onEventWithBuffer = Marshal.GetFunctionPointerForDelegate(_irisCEventHandler.OnEventWithBuffer)
                };

                _irisCEventHandlerNative = Marshal.AllocHGlobal(Marshal.SizeOf(cEventHandlerNativeLocal));
                Marshal.StructureToPtr(cEventHandlerNativeLocal, _irisCEventHandlerNative, true);
                _irisEventHandlerHandleNative =
                    AgoraMpkNative.SetIrisMpkEventHandler(_irisMpkPtr, _irisCEventHandlerNative);

                _callbackObject = new AgoraCallbackObject("Agora" + GetHashCode());
                MediaPlayerObserverNative.CallbackObject = _callbackObject;
            }

            MediaPlayerObserverNative.MediaPlayerObserver = observer;
            return 0;
        }

        public override int UnregisterPlayerObserver()
        {
            if (_irisEventHandlerHandleNative == IntPtr.Zero)
                return (int) MEDIA_PLAYER_ERROR.PLAYER_ERROR_OBJ_NOT_INITIALIZED;

            MediaPlayerObserverNative.MediaPlayerObserver = null;
            MediaPlayerObserverNative.CallbackObject = null;
            if (_callbackObject != null) _callbackObject.Release();
            _callbackObject = null;
            AgoraMpkNative.UnsetIrisMpkEventHandler(_irisMpkPtr, _irisEventHandlerHandleNative);
            Marshal.FreeHGlobal(_irisCEventHandlerNative);
            _irisEventHandlerHandleNative = IntPtr.Zero;
            return (int) MEDIA_PLAYER_ERROR.PLAYER_ERROR_NONE;
        }

        public override int RegisterVideoFrameObserver(IAgoraMpkVideoFrameObserver observer)
        {
            throw new NotImplementedException();
        }

        public override int UnRegisterVideoFrameObserver()
        {
            throw new NotImplementedException();
        }

        public override int RegisterAudioFrameObserver(IAgoraMpkAudioFrameObserver observer)
        {
            throw new NotImplementedException();
        }

        public override int UnRegisterAudioFrameObserver()
        {
            throw new NotImplementedException();
        }

        public override int Connect(string token, string channelId, string userId)
        {
            var param = new
            {
                token,
                channelId,
                userId
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkConnect, JsonMapper.ToJson(param),
                out _result);
        }

        public override int Disconnect()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkDisconnect, JsonMapper.ToJson(param),
                out _result);
        }

        public override int PublishVideo()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkPublishVideo, JsonMapper.ToJson(param),
                out _result);
        }

        public override int UnpublishVideo()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkUnpublishVideo, JsonMapper.ToJson(param),
                out _result);
        }

        public override int PublishAudio()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkPublishAudio, JsonMapper.ToJson(param),
                out _result);
        }

        public override int UnpublishAudio()
        {
            var param = new { };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkUnpublishAudio, JsonMapper.ToJson(param),
                out _result);
        }

        public override int AdjustPublishSignalVolume(int volume)
        {
            var param = new
            {
                volume
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkAdjustPublishSignalVolume,
                JsonMapper.ToJson(param), out _result);
        }

        public override int SetLogFile(string filePath)
        {
            var param = new
            {
                filePath
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetLogFile, JsonMapper.ToJson(param),
                out _result);
        }

        public override int SetLogFilter(LOG_FILTER_TYPE filter)
        {
            var param = new
            {
                filter
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetLogFilter, JsonMapper.ToJson(param),
                out _result);
        }

        public override int ChangePlaybackSpeed(MEDIA_PLAYER_PLAYBACK_SPEED speed)
        {
            var param = new
            {
                speed
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkChangePlaybackSpeed,
                JsonMapper.ToJson(param), out _result);
        }

        public override int SelectAudioTrack(int index)
        {
            var param = new
            {
                index
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSelectAudioTrack, JsonMapper.ToJson(param),
                out _result);
        }

        public override int SetPlayerOption(string key, int value)
        {
            var param = new
            {
                key,
                value
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetPlayerOption, JsonMapper.ToJson(param),
                out _result);
        }

        public override int SetPlayerOption(string key, string value)
        {
            var param = new
            {
                key,
                value
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetPlayerOption, JsonMapper.ToJson(param),
                out _result);
        }

        public override int TakeScreenshot(string filename)
        {
            var param = new
            {
                filename
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkTakeScreenshot, JsonMapper.ToJson(param),
                out _result);
        }

        public override int SelectInternalSubtitle(int index)
        {
            var param = new
            {
                index
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSelectInternalSubtitle,
                JsonMapper.ToJson(param), out _result);
        }

        public override int SetExternalSubtitle(string url)
        {
            var param = new
            {
                url
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetExternalSubtitle,
                JsonMapper.ToJson(param),
                out _result);
        }

        public override int SetLoopCount(int loopCount)
        {
            var param = new
            {
                loopCount
            };
            return AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkSetLoopCount, JsonMapper.ToJson(param),
                out _result);
        }

        public override string GetPlayerSdkVersion()
        {
            var param = new { };
            var res = AgoraMpkNative.CallIrisMpkApi(_irisMpkPtr, ApiTypeMpk.kMpkGetPlayerSdkVersion,
                JsonMapper.ToJson(param), out _result);
            if (res < 0) return null;
            return _result.Result;
        }

        ~AgoraMediaPlayer()
        {
            Dispose(false, true);
        }
    }

    internal static class MediaPlayerObserverNative
    {
        internal static IAgoraMediaPlayerObserver MediaPlayerObserver = null;
        internal static AgoraCallbackObject CallbackObject = null;

        [MonoPInvokeCallback(typeof(Func_Event_Native))]
        internal static void OnEvent(string @event, string data)
        {
            if (CallbackObject == null || CallbackObject._CallbackQueue == null) return;
            switch (@event)
            {
                case "onPlayerStateChanged":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnPlayerStateChanged(
                                (MEDIA_PLAYER_STATE) AgoraJson.GetData<int>(data, "state"),
                                (MEDIA_PLAYER_ERROR) AgoraJson.GetData<int>(data, "ec"));
                        }
                    });
                    break;
                case "onPositionChanged":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnPositionChanged(
                                (long) AgoraJson.GetData<long>(data, "position_ms"));
                        }
                    });
                    break;
                case "onPlayerEvent":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnPlayerEvent(
                                (MEDIA_PLAYER_EVENT) AgoraJson.GetData<int>(data, "event"));
                        }
                    });
                    break;
                case "onPlayBufferUpdated":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnPlayBufferUpdated(
                                (long) AgoraJson.GetData<long>(data, "playCachedBuffer"));
                        }
                    });
                    break;
                case "onCompleted":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnCompleted();
                        }
                    });
                    break;
            }
        }

        [MonoPInvokeCallback(typeof(Func_EventWithBuffer_Native))]
        internal static void OnEventWithBuffer(string @event, string data, IntPtr buffer, uint length)
        {
            var byteData = new byte[length];
            if (buffer != IntPtr.Zero) Marshal.Copy(buffer, byteData, 0, (int) length);
            if (CallbackObject == null || CallbackObject._CallbackQueue == null) return;
            switch (@event)
            {
                case "onMetadata":
                    CallbackObject._CallbackQueue.EnQueue(() =>
                    {
                        if (MediaPlayerObserver != null)
                        {
                            MediaPlayerObserver.OnMetadata(
                                (MEDIA_PLAYER_METADATA_TYPE) AgoraJson.GetData<int>(data, "type"), byteData);
                        }
                    });
                    break;
            }
        }
    }
}