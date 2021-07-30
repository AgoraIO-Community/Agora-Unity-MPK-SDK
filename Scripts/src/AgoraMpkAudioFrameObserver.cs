//  AgoraMpkAudioFrameObserver.cs
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
    internal static class MpkAudioFrameObserverNative
    {
        internal static IAgoraMpkAudioFrameObserver AudioFrameObserver;
        internal static readonly AudioFrame RecordAudioFrame = new AudioFrame();

        [MonoPInvokeCallback(typeof(Func_AudioFrameLocal_Native))]
        internal static bool OnRecordAudioFrame(IntPtr audioFramePtr)
        {
            if (AudioFrameObserver == null) return true;

            var audioFrame = (IrisMpkAudioFrame) (Marshal.PtrToStructure(audioFramePtr, typeof(IrisMpkAudioFrame)) ??
                                                        new IrisMpkAudioFrame());

            if (RecordAudioFrame.channels != audioFrame.channels ||
                RecordAudioFrame.samples != audioFrame.samples ||
                RecordAudioFrame.bytesPerSample != audioFrame.bytes_per_sample)
            {
                RecordAudioFrame.buffer = new byte[audioFrame.buffer_length];
            }

            if (audioFrame.buffer != IntPtr.Zero)
                Marshal.Copy(audioFrame.buffer, RecordAudioFrame.buffer, 0, (int) audioFrame.buffer_length);
            RecordAudioFrame.type = audioFrame.type;
            RecordAudioFrame.samples = audioFrame.samples;
            RecordAudioFrame.bufferPtr = audioFrame.buffer;
            RecordAudioFrame.bytesPerSample = audioFrame.bytes_per_sample;
            RecordAudioFrame.channels = audioFrame.channels;
            RecordAudioFrame.samplesPerSec = audioFrame.samples_per_sec;
            RecordAudioFrame.renderTimeMs = audioFrame.render_time_ms;
            RecordAudioFrame.avsync_type = audioFrame.av_sync_type;

            return AudioFrameObserver.OnRecordAudioFrame(RecordAudioFrame);
        }
    }
}