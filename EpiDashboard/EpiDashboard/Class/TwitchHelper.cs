using System;
namespace EpiDashboard.Class
{
    using Newtonsoft.Json;

    public class TwitchHelper
    {
        public partial class Twitch
        {
            [JsonProperty("stream")]
            public Stream Stream { get; set; }

            [JsonProperty("_links")]
            public TwitchLinks Links { get; set; }
        }

        public partial class TwitchLinks
        {
            [JsonProperty("self")]
            public Uri Self { get; set; }

            [JsonProperty("channel")]
            public Uri Channel { get; set; }
        }

        public partial class Stream
        {
            [JsonProperty("_id")]
            public long Id { get; set; }

            [JsonProperty("game")]
            public string Game { get; set; }

            [JsonProperty("viewers")]
            public long Viewers { get; set; }

            [JsonProperty("video_height")]
            public long VideoHeight { get; set; }

            [JsonProperty("average_fps")]
            public long AverageFps { get; set; }

            [JsonProperty("delay")]
            public long Delay { get; set; }

            [JsonProperty("created_at")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("is_playlist")]
            public bool IsPlaylist { get; set; }

            [JsonProperty("stream_type")]
            public string StreamType { get; set; }

            [JsonProperty("preview")]
            public Preview Preview { get; set; }

            [JsonProperty("channel")]
            public Channel Channel { get; set; }

            [JsonProperty("_links")]
            public StreamLinks Links { get; set; }
        }

        public partial class Channel
        {
            [JsonProperty("mature")]
            public bool Mature { get; set; }

            [JsonProperty("partner")]
            public bool Partner { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("broadcaster_language")]
            public string BroadcasterLanguage { get; set; }

            [JsonProperty("broadcaster_software")]
            public string BroadcasterSoftware { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("game")]
            public string Game { get; set; }

            [JsonProperty("language")]
            public string Language { get; set; }

            [JsonProperty("_id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("created_at")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("updated_at")]
            public DateTimeOffset UpdatedAt { get; set; }

            [JsonProperty("delay")]
            public object Delay { get; set; }

            [JsonProperty("logo")]
            public Uri Logo { get; set; }

            [JsonProperty("banner")]
            public object Banner { get; set; }

            [JsonProperty("video_banner")]
            public Uri VideoBanner { get; set; }

            [JsonProperty("background")]
            public object Background { get; set; }

            [JsonProperty("profile_banner")]
            public Uri ProfileBanner { get; set; }

            [JsonProperty("profile_banner_background_color")]
            public string ProfileBannerBackgroundColor { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("views")]
            public long Views { get; set; }

            [JsonProperty("followers")]
            public long Followers { get; set; }

            [JsonProperty("_links")]
            public ChannelLinks Links { get; set; }
        }

        public partial class ChannelLinks
        {
            [JsonProperty("self")]
            public Uri Self { get; set; }

            [JsonProperty("follows")]
            public Uri Follows { get; set; }

            [JsonProperty("commercial")]
            public Uri Commercial { get; set; }

            [JsonProperty("stream_key")]
            public Uri StreamKey { get; set; }

            [JsonProperty("chat")]
            public Uri Chat { get; set; }

            [JsonProperty("features")]
            public Uri Features { get; set; }

            [JsonProperty("subscriptions")]
            public Uri Subscriptions { get; set; }

            [JsonProperty("editors")]
            public Uri Editors { get; set; }

            [JsonProperty("teams")]
            public Uri Teams { get; set; }

            [JsonProperty("videos")]
            public Uri Videos { get; set; }
        }

        public partial class StreamLinks
        {
            [JsonProperty("self")]
            public Uri Self { get; set; }
        }

        public partial class Preview
        {
            [JsonProperty("small")]
            public Uri Small { get; set; }

            [JsonProperty("medium")]
            public Uri Medium { get; set; }

            [JsonProperty("large")]
            public Uri Large { get; set; }

            [JsonProperty("template")]
            public string Template { get; set; }
        }

    }
}
