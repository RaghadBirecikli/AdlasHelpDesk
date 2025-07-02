using System;

namespace AdlasHelpDesk.Application.Results
{
    public class Meta
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string MessageDetail { get; set; } = string.Empty;
        public int Code { get; set; }


        public static Meta Custom(bool isSuccess, String about)
        {
            return new Meta
            {
                IsSuccess = isSuccess,
                Message = isSuccess ? "İşlem Başarılı" : "İşlem Başarısız",
                MessageDetail = isSuccess ? about : "İşlem Yapmadınız!",
                Code = 0
            };
        }
        public static Meta Success()
        {
            return new Meta
            {
                IsSuccess = true,
                Message = "İşlem Başarılı",
                MessageDetail = "İşlem Başarılı",
                Code = 200
            };
        }
        public static Meta NotFound()
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "Veri Bulunamadı",
                MessageDetail = "Veri Bulunamadı",
                Code = 404
            };
        }
        public static Meta CustomSuccess(String about, int code = 200)
        {
            return new Meta
            {
                IsSuccess = true,
                Message = "İşlem Başarılı",
                MessageDetail = about,
                Code = code
            };

        }
        public static Meta MissingParameter(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "Eksik parametre",
                MessageDetail = about,
                Code = 400
            };

        }

        public static Meta ServerError(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "Server Hatası",
                MessageDetail = about,
                Code = 500
            };

        }
        public static Meta CustomError(String about = "")
        {
            return new Meta
            {
                IsSuccess = false,
                Message = "unexpected error",
                MessageDetail = about,
                Code = 424
            };

        }
    }
}
