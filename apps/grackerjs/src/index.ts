(window as any).setupGracker = function sayHello()
{
  const h4 = document.createElement("h4")
  h4.textContent="Hello from TS!"
  document.body.appendChild(h4)
}
