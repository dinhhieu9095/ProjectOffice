
namespace SurePortal.Core.Kernel.Orgs.Models
{
    public class JsTreeViewModel
    {
        public JsTreeViewModel()
        {
            children = null;
            //  parent = "";
        }

        //  public string parent { get; set; }

        public string parent { get; set; }

        /// <summary>
        /// mã 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// text hiển thị
        /// </summary>
        public string text { get; set; }

        public string node_text { get; set; }

        /// <summary>
        /// biểu tượng icon
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// trạng thái của node
        /// </summary>
        public JsTreeStateObject state { get; set; }

        /// <summary>
        /// node con nếu có
        /// </summary>
        public object children { get; set; }

        /// <summary>
        /// các thuộc tính thẻ li bổ sung
        /// </summary>
        public object li_attr { get; set; }

        /// <summary>
        /// các thuộc tính thẻ a bổ sung
        /// </summary>
        public object a_attr { get; set; }

        public const string jsTreeIconFolder = "";

        public const string jsTreeIconPerson = "";

        public const string jsTreeIconGroup = "";

        public string left_paramvalues { get; set; }

        public bool left_iscount { get; set; }

        public string left_paramnames { get; set; }
    }
    public partial class JsTreeStateObject
    {
        /// <summary>
        /// chỉ định node có được mở hay không
        /// </summary>
        public bool opened { get; set; }

        /// <summary>
        /// chỉ định node có bị tắt tính năng
        /// </summary>
        public bool disabled { get; set; }

        /// <summary>
        /// chỉ định node có được chọn hay không
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// chỉ định node có đóng hay không
        /// </summary>
        public bool closed { get; set; }
    }
}
