namespace nerderies.TelegramBotApi
{
    public class ChatAction
    {
        private ChatAction()
        { }

        internal string Code { get; private set; }

        public static ChatAction Typing => new ChatAction()
        {
            Code = "typing"
        };

        public static ChatAction UploadPhoto => new ChatAction()
        {
            Code = "upload_photo"
        };

        public static ChatAction RecordVideo => new ChatAction()
        {
            Code = "record_video"
        };

        public static ChatAction UploadVideo => new ChatAction()
        {
            Code = "upload_video"
        };

        public static ChatAction RecordAudio => new ChatAction()
        {
            Code = "record_audio"
        };

        public static ChatAction UploadAudio => new ChatAction()
        {
            Code = "upload_audio"
        };

        public static ChatAction UploadDocument => new ChatAction()
        {
            Code = "upload_document"
        };

        public static ChatAction FindLocation => new ChatAction()
        {
            Code = "find_location"
        };

        public static ChatAction RecordVideoNote => new ChatAction()
        {
            Code = "record_video_note"
        };

        public static ChatAction UploadVideoNote => new ChatAction()
        {
            Code = "upload_video_note"
        };
    }
}
