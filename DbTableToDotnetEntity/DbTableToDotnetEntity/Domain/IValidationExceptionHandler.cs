namespace DbTableToDotnetEntity.Domain
{
    /// <summary>
    /// 自定义 IValidationExceptionHandler 接口，ViewModel 继承 IValidationExceptionHandler , 用于接收前端的验证结果。
    /// </summary>
    public interface IValidationExceptionHandler
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsValid
        {
            get;
            set;
        }
    }
}
