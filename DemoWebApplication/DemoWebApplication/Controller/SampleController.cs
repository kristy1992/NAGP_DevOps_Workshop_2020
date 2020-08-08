namespace DemoWebApplication.Controller
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        #region Private Fields

        private const string DDIM_FilePath = "ddim.json";

        #endregion    

        #region Actions

        /// <summary>
        /// Returns DDIM i.e. metadata of all features.
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public ActionResult<string> GetDefaultDesignIntentModel()
        {
            return System.IO.File.ReadAllText(DDIM_FilePath);
        }

        #endregion
    }
}