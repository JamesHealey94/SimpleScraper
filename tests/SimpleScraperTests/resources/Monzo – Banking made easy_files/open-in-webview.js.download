window.onload = function() {
  var url = new URL(window.location)
  var webview = url.searchParams.get('webview')
  if (webview != null) {
    window.location =
      'monzo://webview?url=https://webviews.monzo.com/' + webview
  }
}
