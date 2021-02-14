function formatNumber(originalValue) {
  var value = Math.abs(Number(originalValue))
  switch (true) {
    case value >= 1.0e9:
      return (value / 1.0e9).toFixed(2) + 'B'
    case value >= 1.0e6:
      return (value / 1.0e6).toFixed(2) + 'M'
    case value >= 1.0e3:
      return (value / 1.0e3).toFixed(2) + 'K'
    default:
      return value
  }
}

function refreshCounter() {
  $.ajax({
    url: 'https://api.monzo.com/user-counter/count',
    type: 'GET',
    dataType: 'json',
    success: function(data) {
      var numAccounts = data['count']

      var spans = document.querySelectorAll('.js-num-accounts')

      spans.forEach(function(span) {
        var shouldFormat = span.dataset.shouldFormat || false
        if (!isNaN(numAccounts)) {
          if (shouldFormat) {
            span.textContent = formatNumber(numAccounts)
          } else {
            span.textContent = parseInt(numAccounts).toLocaleString()
          }
        } else {
          console.warn("API didn't return a number", arguments)
        }
      })
    },
    error: function(xhr) {
      console.log('XHR went wrong', arguments)
    },
  })
}

refreshCounter()
