
namespace Imbick.StarCitizen.Api.Models {
    public class Response {
        public bool Success { get; set; }
        public ResponseData Data { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
    }

    public class ResponseData {
        public long TotalRows { get; set; }
        public string Html { get; set; }
    }
}