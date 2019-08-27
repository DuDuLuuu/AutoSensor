using System;
using System.Collections.Generic;
using System.Text;

namespace ZW.Common
{
    public static class ExceptionCode
    {
        #region 公共定义区
        //业务报文XML错误
        public const int SERVICE_MESSAGE_XML_ERROR = 1000;
        //解密错误
        public const int DECRYPT_ERROR = 1001;
        //加密错误
        public const int ENCRYPT_ERROR = 1002;
        //配置文件错误
        public const int CONFIGFILE_ERROR = 1003;
        #endregion
        
        #region USB错误码
        public const int USBDEVICESRLERROR = 5000;
        #endregion

        #region 业务错误码
        //服务处理错误
        public const int SERVICE_RESULT_ERROR = 2000;
        #endregion
    }
}
