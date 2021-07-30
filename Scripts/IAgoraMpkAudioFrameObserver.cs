//  IAgoraMpkAudioFrameObserver.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 28, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

namespace agora.media_player
{
    public class IAgoraMpkAudioFrameObserver
    {
        public virtual bool OnRecordAudioFrame(AudioFrame audioFrame)
        {
            return true;
        }
    }
}