export function capitalizeWords(str) {
    if (typeof str !== 'string') return '';
    if (str.length === 0) return '';
    if (!str) return '';
    return str.replace(/\b\w/g, function(match) {
      return match.toUpperCase();
    });
  }
  