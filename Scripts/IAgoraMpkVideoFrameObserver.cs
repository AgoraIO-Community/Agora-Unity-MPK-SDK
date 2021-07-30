//  IAgoraMpkVideoFrameObserver.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 28, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

namespace agora.media_player
{
    public class IAgoraMpkVideoFrameObserver
    {
        public virtual bool OnCaptureVideoFrame(VideoFrame videoFrame)
        {
            return true;
        }

        public virtual VIDEO_FRAME_TYPE GetVideoFormatPreference()
        {
            return VIDEO_FRAME_TYPE.FRAME_TYPE_RGBA;
        }
    }
}