namespace Portfolio.Playwright.Tests.Fixtures;

/// <summary>Controlled demo HTML used until the portfolio has a dedicated sample application.</summary>
internal static class OrderSearchDemo
{
    public const string Html = """
        <!doctype html>
        <html lang="en">
        <head><meta charset="utf-8"><title>Orders</title></head>
        <body>
          <main>
            <h1>Find an order</h1>
            <form id="search-form">
              <label for="order-reference">Order reference</label>
              <input id="order-reference" type="search" />
              <button type="submit">Search</button>
            </form>
            <ul aria-label="Search results" id="results"></ul>
            <p role="status" id="message"></p>
          </main>
          <script>
            const orders = ['ORD-1001', 'ORD-1002', 'ORD-2001'];
            document.querySelector('#search-form').addEventListener('submit', event => {
              event.preventDefault();
              const value = document.querySelector('#order-reference').value.trim().toLowerCase();
              const matches = orders.filter(order => order.toLowerCase().includes(value));
              document.querySelector('#results').replaceChildren(
                ...matches.map(order => {
                  const item = document.createElement('li');
                  item.textContent = order;
                  return item;
                })
              );
              document.querySelector('#message').textContent = matches.length === 0
                ? 'No orders found'
                : `${matches.length} order(s) found`;
            });
          </script>
        </body>
        </html>
        """;
}
