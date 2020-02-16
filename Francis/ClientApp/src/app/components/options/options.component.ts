import { Component } from '@angular/core';
import { OmbiService, TelegramService } from '../../services/options.service';


@Component({
  selector: 'app-options',
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.scss']
})
export class OptionsComponent {

  constructor(
    public telegram: TelegramService,
    public ombi: OmbiService,
  ) {
    this.telegram.getOptions();
    this.ombi.getOptions();
  }

}
