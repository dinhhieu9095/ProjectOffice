(function () {
    "use strict";
    app.factory("fileFactory", fileFactory);

    function fileFactory() {
        var self = {};
        self.types = {
            image: ["png", "gif", "jpg", "jpeg", "bmp", "ico"],
            document: ["pdf", "doc", "docx", "xls", "xlsx", "txt", "ppt", "pptx"],
            media: ["mp3", "mpg", "mpeg", "mp4", "wav", "wmv", "vcf", "fla"],
            zip: ["zip", "tar", "gz", "tgz", "rar"],
            graphic: ["ai", "eps", "psd", "sit", "tif", "tiff"]
        };
        self.acceptedTypes = ["pdf", "doc", "docx", "ai", "eps", "png", "gif", "jpg", "jpeg", "rtf", "txt", "mp3", "mpg", "mpeg", "mp4", "ppt", "pptx", "psd", "sit", "tif", "tiff", "vcf", "wav", "wmv", "xls", "xlsx", "zip", "fla", "tar", "gz", "tgz", "mpp", "xml"];
        self.validLengths = {
            image: 5242880
        };
        self.maximumSize = 10485760; //10MB
        return self;
    }
})();