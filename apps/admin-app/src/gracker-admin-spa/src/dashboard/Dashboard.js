import { Component } from 'react'

export class Dashboard extends Component {

  RealTimeUsers = 1

  render() {
    return (
      <div class="flex items-center justify-center min-h-screen">
        <p class="text-9xl font-bold">{this.RealTimeUsers}</p>
      </div>
    );
  }
}
