//  IAgoraMediaPlayerObserver.cs
//
//  Created by Yiqing Huang on July 27, 2021.
//  Modified by Yiqing Huang on July 27, 2021.
//
//  Copyright © 2021 Agora. All rights reserved.
//

namespace agora.media_player
{
    public abstract class IAgoraMediaPlayerObserver
    {
        public virtual void OnPlayerStateChanged(MEDIA_PLAYER_STATE state, MEDIA_PLAYER_ERROR ec)
        {
        }

        public virtual void OnPositionChanged(long positionMs)
        {
        }

        public virtual void OnPlayerEvent(MEDIA_PLAYER_EVENT @event)
        {
        }

        public virtual void OnMetadata(MEDIA_PLAYER_METADATA_TYPE type, byte[] data)
        {
        }

        public virtual void OnPlayBufferUpdated(long playCachedBuffer)
        {
        }

        public virtual void OnCompleted()
        {
        }
    }
}