import { Component } from '@angular/core';
import { MessengerService } from './services/messenger.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  loading: boolean = false;

  constructor(private messenger: MessengerService) {
    this.messenger.loading.subscribe(loading => this.loading = loading);
  }

}
