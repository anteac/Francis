import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AboutOmbi } from '../../models/generated/AboutOmbi';
import { OmbiOptions } from '../../models/generated/OmbiOptions';
import { MessengerService } from '../../services/messenger/messenger.service';

@Component({
  selector: 'app-ombi',
  templateUrl: './ombi.component.html',
  styleUrls: ['./ombi.component.scss']
})
export class OmbiComponent {

  options: OmbiOptions = {};
  about: AboutOmbi = null;
  error: string = null;

  constructor(private http: HttpClient, private messenger: MessengerService) {
    this.getOptions();
  }

  getOptions(): void {
    this.messenger.loading.next(true);
    this.http.get('options/ombi').subscribe(options => {
      this.options = options;
      this.messenger.loading.next(false);
    });
  }

  pushOptions(): void {
    this.about = null;
    this.error = null;

    this.messenger.loading.next(true);
    this.http.post('options/ombi', this.options).subscribe(about => {
      this.about = about;
      this.messenger.loading.next(false);
    }, () => {
        this.error = 'An error occured. Please check your settings.'
        this.messenger.loading.next(false);
    });
  }

}
