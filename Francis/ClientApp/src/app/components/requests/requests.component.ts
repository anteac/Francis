import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
//import { ShowRequestProgressionDialog } from '../../dialogs/show-log-message/show-log-message.dialog';
import { RequestProgression } from '../../models/generated/RequestProgression';
import { MessengerService } from '../../services/messenger.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent {

  error: string;
  requests: RequestProgression[];

  constructor(private http: HttpClient, private messenger: MessengerService, public dialog: MatDialog) {
    this.getRequests();
  }

  getRequests(): void {
    this.messenger.loading.next(true);
    this.http.get<RequestProgression[]>('requests').subscribe(requests => {
      this.requests = requests;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of requests.'
      this.messenger.loading.next(false);
    });
  }

  //showRequest(request: RequestProgression) {
  //  this.dialog.open(ShowRequestProgressionDialog, { data: request });
  //}
}
