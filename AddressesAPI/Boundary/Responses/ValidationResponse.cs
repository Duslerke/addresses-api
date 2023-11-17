namespace AddressesAPI.Boundary.Responses
{
    public class ValidationResponse
    {
        public IList<string> Errors { get; set; }

        public ValidationResponse(IList<string> errors)
        {
            Errors = errors;
        }
    }
}
