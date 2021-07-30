//  AgoraBase.cs
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
    /** Definition of MediaPlayerContext.
 */
    public class MediaPlayerContext
    {
        public MediaPlayerContext()
        {
            enable_chat_voice_mode = false;
            context = 0;
        }

        public MediaPlayerContext(bool enableChatVoiceMode, IntPtr context)
        {
            enable_chat_voice_mode = enableChatVoiceMode;
            this.context = (ulong) context;
        }

        /** User Context, i.e., activity context in Android.
   */
        public bool enable_chat_voice_mode { set; get; }

        public ulong context { set; get; }
    }

    /** The video pixel format.
   *
   * @note The SDK does not support the alpha channel, and discards any alpha value passed to the SDK.
   */
    public enum VIDEO_PIXEL_FORMAT
    {
        /** 0: The video pixel format is unknown.
     */
        VIDEO_PIXEL_UNKNOWN = 0,

        /** 1: The video pixel format is I420.
     */
        VIDEO_PIXEL_I420 = 1,

        /** 2: The video pixel format is BGRA.
     */
        VIDEO_PIXEL_BGRA = 2,

        /** 3: The video pixel format is NV21.
     */
        VIDEO_PIXEL_NV21 = 3,

        /** 4: The video pixel format is RGBA.
     */
        VIDEO_PIXEL_RGBA = 4,

        /** 5: The video pixel format is IMC2.
     */
        VIDEO_PIXEL_IMC2 = 5,

        /** 7: The video pixel format is ARGB.
     */
        VIDEO_PIXEL_ARGB = 7,

        /** 8: The video pixel format is NV12.
     */
        VIDEO_PIXEL_NV12 = 8,

        /** 16: The video pixel format is I422.
     */
        VIDEO_PIXEL_I422 = 16,
    }

    /** Video display modes. */
    public enum RENDER_MODE_TYPE
    {
        /**
1: Uniformly scale the video until it fills the visible boundaries (cropped). One dimension of the video may have clipped contents.
 */
        RENDER_MODE_HIDDEN = 1,

        /**
2: Uniformly scale the video until one of its dimension fits the boundary (zoomed to fit). Areas that are not filled due to disparity in the aspect ratio are filled with black.
*/
        RENDER_MODE_FIT = 2,

        /** **DEPRECATED** 3: This mode is deprecated.
   */
        RENDER_MODE_ADAPTIVE = 3,

        /**
  4: The fill mode. In this mode, the SDK stretches or zooms the video to fill the display window.
  */
        RENDER_MODE_FILL = 4,
    }

    /**
 * @brief Player state
 *
 */
    public enum MEDIA_PLAYER_STATE
    {
        /** Player idle
   */
        PLAYER_STATE_IDLE = 0,

        /** Opening media file
   */
        PLAYER_STATE_OPENING,

        /** Media file opened successfully
   */
        PLAYER_STATE_OPEN_COMPLETED,

        /** Player playing
   */
        PLAYER_STATE_PLAYING,

        /** Player paused
   */
        PLAYER_STATE_PAUSED,

        /** Player playback one loop completed
   */
        PLAYER_STATE_PLAYBACK_COMPLETED,

        /** Player playback all loops completed
   */
        PLAYER_STATE_PLAYBACK_ALL_LOOPS_COMPLETED,

        /** Player stopped
   */
        PLAYER_STATE_STOPPED = PLAYER_STATE_IDLE,

        /** Player pausing (internal)
   */
        PLAYER_STATE_PAUSING_INTERNAL = 50,

        /** Player stopping (internal)
   */
        PLAYER_STATE_STOPPING_INTERNAL,

        /** Player seeking state (internal)
   */
        PLAYER_STATE_SEEKING_INTERNAL,

        /** Player getting state (internal)
   */
        PLAYER_STATE_GETTING_INTERNAL,

        /** None state for state machine (internal)
   */
        PLAYER_STATE_NONE_INTERNAL,

        /** Do nothing state for state machine (internal)
   */
        PLAYER_STATE_DO_NOTHING_INTERNAL,

        /** Player failed
   */
        PLAYER_STATE_FAILED = 100,
    };

    /**
 * @brief Player error code
 *
 */
    public enum MEDIA_PLAYER_ERROR
    {
        /** No error
   */
        PLAYER_ERROR_NONE = 0,

        /** The parameter is incorrect
   */
        PLAYER_ERROR_INVALID_ARGUMENTS = -1,

        /** Internel error
   */
        PLAYER_ERROR_INTERNAL = -2,

        /** No resource error
   */
        PLAYER_ERROR_NO_RESOURCE = -3,

        /** Media source is invalid
   */
        PLAYER_ERROR_INVALID_MEDIA_SOURCE = -4,

        /** Unknown stream type
   */
        PLAYER_ERROR_UNKNOWN_STREAM_TYPE = -5,

        /** Object is not initialized
   */
        PLAYER_ERROR_OBJ_NOT_INITIALIZED = -6,

        /** Decoder codec not supported
   */
        PLAYER_ERROR_CODEC_NOT_SUPPORTED = -7,

        /** Video renderer is invalid
   */
        PLAYER_ERROR_VIDEO_RENDER_FAILED = -8,

        /** Internal state error
   */
        PLAYER_ERROR_INVALID_STATE = -9,

        /** Url not found
   */
        PLAYER_ERROR_URL_NOT_FOUND = -10,

        /** Invalid connection state
   */
        PLAYER_ERROR_INVALID_CONNECTION_STATE = -11,

        /** Insufficient buffer data
   */
        PLAYER_ERROR_SRC_BUFFER_UNDERFLOW = -12,
    };

    /**
 * @brief Playback speed type
 *
 */
    public enum MEDIA_PLAYER_PLAYBACK_SPEED
    {
        /** original playback speed
   */
        PLAYBACK_SPEED_ORIGINAL = 100,

        /** playback speed slow down to 0.5
 */
        PLAYBACK_SPEED_50_PERCENT = 50,

        /** playback speed slow down to 0.75
   */
        PLAYBACK_SPEED_75_PERCENT = 75,

        /** playback speed speed up to 1.25
   */
        PLAYBACK_SPEED_125_PERCENT = 125,

        /** playback speed speed up to 1.5
   */
        PLAYBACK_SPEED_150_PERCENT = 150,

        /** playback speed speed up to 2.0
   */
        PLAYBACK_SPEED_200_PERCENT = 200,
    };

    /**
 * @brief Media stream type
 *
 */
    public enum MEDIA_STREAM_TYPE
    {
        /** Unknown stream type
   */
        STREAM_TYPE_UNKNOWN = 0,

        /** Video stream
   */
        STREAM_TYPE_VIDEO = 1,

        /** Audio stream
   */
        STREAM_TYPE_AUDIO = 2,

        /** Subtitle stream
   */
        STREAM_TYPE_SUBTITLE = 3,
    };

    /**
 * @brief Player event
 *
 */
    public enum MEDIA_PLAYER_EVENT
    {
        /** player seek begin
   */
        PLAYER_EVENT_SEEK_BEGIN = 0,

        /** player seek complete
   */
        PLAYER_EVENT_SEEK_COMPLETE = 1,

        /** player seek error
   */
        PLAYER_EVENT_SEEK_ERROR = 2,

        /** player video published
   */
        PLAYER_EVENT_VIDEO_PUBLISHED = 3,

        /** player audio published
   */
        PLAYER_EVENT_AUDIO_PUBLISHED = 4,

        /** player audio track changed
   */
        PLAYER_EVENT_AUDIO_TRACK_CHANGED = 5,

        /** player buffer low
   */
        PLAYER_EVENT_BUFFER_LOW = 6,

        /** player buffer recover
   */
        PLAYER_EVENT_BUFFER_RECOVER = 7,
    };

    /**
 * @brief Media stream object
 *
 */
    public class MediaStreamInfo
    {
        /* the index of the stream in the media file */
        public int streamIndex { get; set; }

        /* stream type */
        public MEDIA_STREAM_TYPE streamType { get; set; }

        /* stream encoding name */
        public string codecName { get; set; }

        /* streaming language */
        public string language { get; set; }

        /* If it is a video stream, video frames rate */
        public int videoFrameRate { get; set; }

        /* If it is a video stream, video bit rate */
        public int videoBitRate { get; set; }

        /* If it is a video stream, video width */
        public int videoWidth { get; set; }

        /* If it is a video stream, video height */
        public int videoHeight { get; set; }

        /* If it is a video stream, video rotation */
        public int videoRotation { get; set; }

        /* If it is an audio stream, audio bit rate */
        public int audioSampleRate { get; set; }

        /* If it is an audio stream, the number of audio channels */
        public int audioChannels { get; set; }

        /* If it is an audio stream, bits per sample */
        public int audioBitsPerSample { get; set; }

        /* stream duration in second */
        public long duration { get; set; }
    }

    /**
 * @brief Player Metadata type
 *
 */
    public enum MEDIA_PLAYER_METADATA_TYPE
    {
        /** data type unknown
   */
        PLAYER_METADATA_TYPE_UNKNOWN = 0,

        /** sei data
   */
        PLAYER_METADATA_TYPE_SEI = 1,
    }

    /** Output log filter level. */
    [Flags]
    public enum LOG_FILTER_TYPE
    {
        /** 0: Do not output any log information. */
        LOG_FILTER_OFF = 0,

        /** 0x080f: Output all log information.
         * Set your log filter as debug if you want to get the most complete log file.      */
        LOG_FILTER_DEBUG = 0x080f,

        /** 0x000f: Output CRITICAL, ERROR, WARNING, and INFO level log information.
         * We recommend setting your log filter as this level.
         */
        LOG_FILTER_INFO = 0x000f,

        /** 0x000e: Outputs CRITICAL, ERROR, and WARNING level log information.
         */
        LOG_FILTER_WARN = 0x000e,

        /** 0x000c: Outputs CRITICAL and ERROR level log information. */
        LOG_FILTER_ERROR = 0x000c,

        /** 0x0008: Outputs CRITICAL level log information. */
        LOG_FILTER_CRITICAL = 0x0008,

        /// @cond
        LOG_FILTER_MASK = 0x80f,
        /// @endcond
    }

    /** The output log level of the SDK.
 *
 * @since v3.3.0
 */
    public enum LOG_LEVEL
    {
        /** 0: Do not output any log. */
        LOG_LEVEL_NONE = 0x0000,

        /** 0x0001: (Default) Output logs of the FATAL, ERROR, WARN and INFO level. We recommend setting your log filter as this level.
   */
        LOG_LEVEL_INFO = 0x0001,

        /** 0x0002: Output logs of the FATAL, ERROR and WARN level.
   */
        LOG_LEVEL_WARN = 0x0002,

        /** 0x0004: Output logs of the FATAL and ERROR level.  */
        LOG_LEVEL_ERROR = 0x0004,

        /** 0x0008: Output logs of the FATAL level.  */
        LOG_LEVEL_FATAL = 0x0008,
    }

    /** Video frame containing the Agora RTC SDK's encoded video data. */
    public class VideoFrame
    {
        /** The video frame type: #VIDEO_FRAME_TYPE. */
        public VIDEO_FRAME_TYPE type;

        /** Width (pixel) of the video frame.*/
        public int width;

        /** Height (pixel) of the video frame. */
        public int height;

        /** Line span of the Y buffer within the YUV data.
     */
        public int yStride; //stride of Y data buffer

        /** Line span of the U buffer within the YUV data.
     */
        public int uStride; //stride of U data buffer

        /** Line span of the V buffer within the YUV data.
     */
        public int vStride; //stride of V data buffer

        /** Pointer to the Y buffer pointer within the YUV data.
     */
        public byte[] yBuffer; //Y data buffer

        public IntPtr yBufferPtr;

        /** Pointer to the U buffer pointer within the YUV data.
     */
        public byte[] uBuffer; //U data buffer

        public IntPtr uBufferPtr;

        /** Pointer to the V buffer pointer within the YUV data.
     */
        public byte[] vBuffer; //V data buffer

        public IntPtr vBufferPtr;

        /** Set the rotation of this frame before rendering the video. Supports 0, 90, 180, 270 degrees clockwise.
     */
        /** Set the rotation of this frame before rendering the video. Supports 0, 90, 180, 270 degrees clockwise.
         */
        public int rotation; // rotation of this frame (0, 90, 180, 270)

        /** The timestamp of the external audio frame. It is mandatory. You can use this parameter for the following purposes:
         * - Restore the order of the captured audio frame.
         * - Synchronize audio and video frames in video-related scenarios, including scenarios where external video sources are used.
         * @note This timestamp is for rendering the video stream, and not for capturing the video stream.
         */
        public long renderTimeMs;

        /** Reserved for future use. */
        public int avsync_type;
    }

    /** The video frame type. */
    public enum VIDEO_FRAME_TYPE
    {
        /**
     * 0: YUV420
     */
        FRAME_TYPE_YUV420 = 0, // YUV 420 format

        /**
     * 1: YUV422
     */
        FRAME_TYPE_YUV422 = 1, // YUV 422 format

        /**
     * 2: RGBA
     */
        FRAME_TYPE_RGBA = 2, // RGBA format
    }

    /** Definition of AudioFrame */
    public class AudioFrame
    {
        public AudioFrame()
        {
        }

        public AudioFrame(AUDIO_FRAME_TYPE type, int samples, int bytesPerSample, int channels, int samplesPerSec,
            byte[] buffer, long renderTimeMs, int avsync_type)
        {
            this.type = type;
            this.samples = samples;
            this.bytesPerSample = bytesPerSample;
            this.channels = channels;
            this.samplesPerSec = samplesPerSec;
            this.buffer = buffer;
            this.renderTimeMs = renderTimeMs;
            this.avsync_type = avsync_type;
        }

        /** The type of the audio frame. See #AUDIO_FRAME_TYPE
		 */
        public AUDIO_FRAME_TYPE type { set; get; }

        /** The number of samples per channel in the audio frame.
		 */
        public int samples { set; get; } //number of samples for each channel in this frame

        /**The number of bytes per audio sample, which is usually 16-bit (2-byte).
		 */
        public int bytesPerSample { set; get; } //number of bytes per sample: 2 for PCM16

        /** The number of audio channels.
		 - 1: Mono
		 - 2: Stereo (the data is interleaved)
		 */
        public int channels { set; get; } //number of channels (data are interleaved if stereo)

        /** The sample rate.
		 */
        public int samplesPerSec { set; get; } //sampling rate

        /** The data buffer of the audio frame. When the audio frame uses a stereo channel, the data buffer is interleaved.
		 The size of the data buffer is as follows: `buffer` = `samples` × `channels` × `bytesPerSample`.
		 */
        public byte[] buffer { set; get; } //data buffer

        public IntPtr bufferPtr { set; get; }

        /** The timestamp of the external audio frame. You can use this parameter for the following purposes:
		 - Restore the order of the captured audio frame.
		 - Synchronize audio and video frames in video-related scenarios, including where external video sources are used.
		 */
        public long renderTimeMs { set; get; }

        /** Reserved parameter.
		 */
        public int avsync_type { set; get; }
    }

    public enum AUDIO_FRAME_TYPE
    {
        /** 0: PCM16. */
        FRAME_TYPE_PCM16 = 0, // PCM 16bit little endian
    }
}