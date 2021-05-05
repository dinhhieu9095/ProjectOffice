(function() {
  var ObjectToFormData, ObjectToFormDataApp;

  ObjectToFormDataApp = (function() {
    function ObjectToFormDataApp() {
      return [];
    }

    return ObjectToFormDataApp;

  })();

  angular.module('object-to-form-data', new ObjectToFormDataApp());

  ObjectToFormData = (function() {
    function ObjectToFormData() {
      var goDeep, objectToFormData;
      goDeep = function(property) {
        if (typeof property !== 'object') {
          return false;
        }
        return !(property instanceof File || property instanceof Blob || property instanceof Date);
      };
      objectToFormData = function (obj, form, namespace) {
        var fd, formKey, property;
        fd = form || new FormData();
        for (property in obj) {
          if (obj.hasOwnProperty(property)) {
            if (namespace) {
              formKey = namespace + "[" + property + "]";
            } else {
              formKey = property;
            }
            if (goDeep(obj[property])) {
              objectToFormData(obj[property], fd, formKey);
            } else {
              fd.append(formKey, obj[property]);
            }
          }
        }
        return fd;
      };
      return function(object) {
        return objectToFormData(object);
      };
    }

    return ObjectToFormData;

  })();

  angular.module('object-to-form-data').factory('objectToFormData', [ObjectToFormData]);

}).call(this);
