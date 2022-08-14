import { Component } from 'react'

export class Dashboard extends Component {

  constructor(props) {
    super(props);
    this.state = { activeUsers: 0 };
  }

  render() {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <p className="text-9xl font-bold">{this.state.activeUsers}</p>
      </div>
    );
  }

  componentDidMount() {
    fetch(`${process.env.REACT_APP_API_BASE}/active-users`)
      .then(response => response.json())
      .then(response => this.setState({ activeUsers: response.count }));
  }
}
