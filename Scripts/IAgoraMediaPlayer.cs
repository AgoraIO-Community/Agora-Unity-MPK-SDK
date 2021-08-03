//  IAgoraMediaPlayer.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 27, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

using System;
using agora.rtc;

namespace agora.media_player
{
    using view_t = UInt64;

    public abstract class IAgoraMediaPlayer
    {
        public abstract int Initialize(MediaPlayerContext context);
        public abstract int Open(string src, long startPos = 0);
        public abstract int Play();
        public abstract int Pause();
        public abstract int Stop();
        public abstract int Seek(long pos);
        public abstract int Mute(bool mute = false);
        public abstract bool GetMute();
        public abstract int AdjustPlayoutVolume(int volume);
        public abstract int GetPlayoutVolume();
        public abstract long GetDuration();
        public abstract long GetPlayPosition();
        public abstract long GetStreamCount();
        public abstract int GetStreamInfo(long index, out MediaStreamInfo info);
        public abstract int SetView(view_t view);
        public abstract int SetRenderMode(RENDER_MODE_TYPE renderMode);
        public abstract int RegisterPlayerObserver(IAgoraMediaPlayerObserver observer);
        public abstract int UnregisterPlayerObserver();
        public abstract int RegisterVideoFrameObserver(IAgoraMpkVideoFrameObserver observer, bool needByteArray = true);
        public abstract int UnRegisterVideoFrameObserver();
        public abstract int RegisterAudioFrameObserver(IAgoraMpkAudioFrameObserver observer, bool needByteArray = true);
        public abstract int UnRegisterAudioFrameObserver();
        public abstract int Connect(string token, string channelId, string userId);
        public abstract int Disconnect();
        public abstract int PublishVideo();
        public abstract int UnpublishVideo();
        public abstract int PublishAudio();
        public abstract int UnpublishAudio();
        public abstract int AdjustPublishSignalVolume(int volume);
        public abstract int SetLogFile(string filePath);
        public abstract int SetLogFilter(LOG_FILTER_TYPE filter);
        public abstract int ChangePlaybackSpeed(MEDIA_PLAYER_PLAYBACK_SPEED speed);
        public abstract int SelectAudioTrack(int index);
        public abstract int SetPlayerOption(string key, int value);
        public abstract int SetPlayerOption(string key, string value);
        public abstract int TakeScreenshot(string filename);
        public abstract int SelectInternalSubtitle(int index);
        public abstract int SetExternalSubtitle(string url);
        public abstract int SetLoopCount(int loopCount);
        public abstract string GetPlayerSdkVersion();
        public abstract void Dispose(bool sync = true);
    }
}