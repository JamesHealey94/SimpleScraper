/**
 * This file sets a global variable utils which provides a variaty of utility functions
 */

window.Utils = {
  createUuid: function() {
    // Copy & paste form http://stackoverflow.com/questions/105034/create-guid-uuid-in-javascript/2117523#2117523
    // This is a cryptograhically not secure implementation, but is good enough for the given use case
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'
      .replace(/[xy]/g, function(c) {
        var r = Math.random() * 16|0, v = c == 'x' ? r : (r&0x3|0x8)
        return v.toString(16)
      })
  },

  getUrlQueryParam: function(param) {
    // Parse query parameters
    var queryParams = {}
    window.location.search.replace(/[?&]+([^=&]+)=?([^&]*)?/gi, function(m, key, value) {
      queryParams[key] = value !== undefined ? value : ''
    })

    // Return the requested parameter
    return decodeURIComponent(param && queryParams[param] ? queryParams[param] : null)
  }
}
