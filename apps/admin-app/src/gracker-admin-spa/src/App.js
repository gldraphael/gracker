import './App.css';

import { Button } from "@material-tailwind/react";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Edit <code>src/App.js</code> and save to reload. Okay?
        </p>
        <div className="flex w-max gap-4">
          <Button color="blue">color blue</Button>
          <Button color="red">color red</Button>
          <Button color="green">color green</Button>
          <Button color="amber">color amber</Button>
        </div>
      </header>
    </div>
  );
}

export default App;
