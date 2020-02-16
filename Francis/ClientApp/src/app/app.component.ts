import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component } from '@angular/core';
import { MessengerService } from './services/messenger.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  navLinks = [
    { path: '/', label: 'Home' },
    { path: '/options', label: 'Options' },
    { path: '/logs', label: 'Logs' },
  ];

  loading: boolean = false;
  onMobile: boolean = false;

  constructor(private messenger: MessengerService, breakpointObserver: BreakpointObserver) {
    this.messenger.loading.subscribe(loading => this.loading = loading);

    breakpointObserver.observe([
      Breakpoints.HandsetLandscape,
      Breakpoints.HandsetPortrait
    ]).subscribe(result => {
      this.onMobile = result.matches;
    });
  }

}
