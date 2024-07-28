using System;

namespace BMS.DTO.Response
{
    /// <summary>
    /// All Add, Update, Delete request produce this response. 
    /// </summary>
    [Serializable]
    public class BaseResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseResponse()
        {
            ResponseType = Response.ResponseType.Success;
            ResponseCode = Response.ResponseCode.Success;
            ResponseData = null;
        }
        /// <summary>
        /// Response Type (Success,Failed,Error,Un-Authorize) , API consumer must check this type and need to make decision according to this status.
        /// Success: Successfully return the required data. 
        /// Failed: This type of response is happen when any validation failed i.e Model Required Field or Password not valid or OTP is not valid or any other business rule related validation error.
        /// Error : Is there any internal exception happen, this type of response type will be happen.
        /// Un-Authorize : This type of return type is only for API consumer not for end user. i.e 
        /// Invalid token or token has expired or 
        /// You are not authorize to access this api or 
        /// To access this api you have to get token with subscriber credential.
        /// </summary>
        public string ResponseType { get; set; }
        /// <summary>
        /// Response code, for success case it will be always 200
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// If response type is not 'Success' this field will contains the actual reason
        /// </summary>
        public string ResponseMessage { get; set; }

 
        /// <summary>
        /// Response Data will provide the necessary data
        /// Model Validation Failed Will Produce : 
        /// </summary>
        public object ResponseData { get; set; }
   
    }

    public static class SetResponses
    {
        public static BaseResponse SetResponse(string type, string msg, string code, ActionType actionType = ActionType.None)
        {
            return new BaseResponse { ResponseType = type, ResponseCode = code, ResponseMessage = msg };
        }
        public static BaseResponse SetSuccessResponse(string msg = "Success!")
        {
            return new BaseResponse { ResponseType = ResponseType.Success, ResponseCode = ResponseCode.Success, ResponseMessage = msg };
        }
        public static BaseResponse SetSuccessWithDataResponse(object data, string message = "Success!")
        {
            return new BaseResponse { ResponseType = ResponseType.Success, ResponseCode = ResponseCode.Success, ResponseMessage = message, ResponseData = data };
        }
        public static BaseResponse SetFaildResponse(string msg = "Faild!", object data = null)
        {
            return new BaseResponse { ResponseType = ResponseType.Failed, ResponseCode = ResponseCode.NotFound, ResponseMessage = msg,  ResponseData = data };
        }

        public static BaseResponse SetErrorResponse(string msg = "Error!", string code = ResponseCode.NotFound, ActionType actionType = ActionType.None, object data = null)
        {
            return new BaseResponse { ResponseType = ResponseType.Error, ResponseCode = code, ResponseMessage = msg,  ResponseData = data };
        }
        public static BaseResponse SetInformationResponse(string msg = "Info!", string code = ResponseCode.NotFound, ActionType actionType = ActionType.None)
        {
            return new BaseResponse { ResponseType = ResponseType.Info, ResponseCode = code, ResponseMessage = msg };
        }
        public static BaseResponse SetWarningResponse(string msg = "Warning!", string code = ResponseCode.NotFound, ActionType actionType = ActionType.None)
        {
            return new BaseResponse { ResponseType = ResponseType.Warning, ResponseCode = code, ResponseMessage = msg };
        }

        public static BaseResponse SetExceptionResponse(string msg = "Exception!", object data = null)
        {
            return new BaseResponse { ResponseType = ResponseType.Error, ResponseCode = ResponseCode.InternalServerError, ResponseMessage = msg, ResponseData = data };
        }

        public static BaseResponse SetUnAuthorizeAccessResponse(string msg = "Un-Authorize Access!", object data = null)
        {
            return new BaseResponse { ResponseType = ResponseType.UnAuthorizeAccess, ResponseCode = ResponseCode.UnAuthorizeAccess, ResponseMessage = msg, ResponseData = data };
        }
    }


    public enum ActionType
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Get = 4
    }

    public static class ResponseType
    {
        public const string Success = "Success";
        public const string Failed = "Failed";
        public const string Error = "Error";
        public const string Info = "Info";
        public const string Warning = "Warning";
        public const string UnAuthorizeAccess = "Un-Authorize";

    }

    public static class ResponseCode
    {
        public const string Success = "200";
        public const string BadRequest = "400";
        public const string UnAuthorizeAccess = "401";
        public const string NotFound = "404";
        public const string InternalServerError = "500";
    }
}
