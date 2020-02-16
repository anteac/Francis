import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { LogMessage } from '../../models/generated/LogMessage';

@Component({
  selector: 'app-show-log-message-dialog',
  templateUrl: 'show-log-message.dialog.html',
  styleUrls: ['show-log-message.dialog.scss']
})
export class ShowLogMessageDialog {

  constructor(@Inject(MAT_DIALOG_DATA) public log: LogMessage) { }

}
