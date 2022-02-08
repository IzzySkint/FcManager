namespace FcManager.Services
{
    public class ValidatorResult
    {
        public ValidatorResult(bool isValid, string action, string[] errors)
        {
            this.IsValid = isValid;
            this.Action = action;
            this.Errors = errors;
        }
        
        public string Action { get; private set; }
        public bool IsValid { get; private set; }
        public string[] Errors { get; private set; }
    }
}