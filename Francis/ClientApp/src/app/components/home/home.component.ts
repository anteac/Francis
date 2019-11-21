import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  loadingTelegram: boolean = false;
  telegramStatus: boolean = false;

  loadingOmbi: boolean = false;
  ombiStatus: boolean = false;

  constructor(private http: HttpClient) {
    this.getStatuses();
  }

  getStatuses(): void {

    this.loadingTelegram = true;
    this.http.get<boolean>('monitoring/telegram/status').subscribe(
      status => {
        this.telegramStatus = status;
        this.loadingTelegram = false;
      },
      () => {
        this.telegramStatus = false;
        this.loadingTelegram = false;
      }
    );

    this.loadingOmbi = true;
    this.http.get('monitoring/ombi/status').subscribe(
      () => {
        this.ombiStatus = true;
        this.loadingOmbi = false;
      },
      () => {
        this.ombiStatus = false;
        this.loadingOmbi = false;
      }
    );

  }

}
