using System.Web.Mvc;
using Cogworks.Umbraco.Essentials.Constants;

namespace Cogworks.Umbraco.Essentials.ActionResults
{
    public class RedirectWithMessageResult : ActionResult
    {
        private readonly string _message;
        private readonly string _header;

        public ActionResult BaseResult { get; }

        public RedirectWithMessageResult(ActionResult redirectBaseResult, string header, string message)
        {
            BaseResult = redirectBaseResult;

            _header = header;
            _message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData[TempDataConstants.MessageConstants.Header] = _header;
            context.Controller.TempData[TempDataConstants.MessageConstants.Message] = _message;

            BaseResult.ExecuteResult(context);
        }
    }
}