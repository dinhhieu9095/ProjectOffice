using System.Text;
using System.Web.Mvc;

namespace SurePortal.Core.Kernel.Filters
{
    /// <summary>Processes the output of an action before it is transmitted to the client.</summary>
    public abstract class OutputProcessorActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="OutputProcessorActionFilterAttribute" /> class.</summary>
        protected OutputProcessorActionFilterAttribute()
        {
            InputEncoding = Encoding.UTF8;
            OutputEncoding = Encoding.UTF8;
        }

        /// <summary>Gets or sets the input encoding.</summary>
        public Encoding InputEncoding { get; set; }

        /// <summary>Gets or sets the output encoding.</summary>
        public Encoding OutputEncoding { get; set; }

        /// <summary>Processes the output data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The processed data.</returns>
        protected abstract string Process(string data);

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Filter = new OutputProcessorStream(response.Filter, InputEncoding, OutputEncoding, Process);
        }
    }
}