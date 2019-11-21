import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  navLinks = [
    { path: '/', label: 'Home' },
    { path: '/telegram', label: 'Telegram Options' },
    { path: '/ombi', label: 'Ombi Options' },
    { path: '/logs', label: 'Logs' },
  ];

}
