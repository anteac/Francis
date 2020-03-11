import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { RequestProgression } from '../../models/generated/RequestProgression';
import { RequestStatus } from '../../models/generated/RequestStatus';
import { MessengerService } from '../../services/messenger.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent {

  RequestStatus = RequestStatus;

  error: string;
  requests: RequestProgression[];

  constructor(private http: HttpClient, private messenger: MessengerService, public dialog: MatDialog) {
    this.getRequests();
  }

  getRequests(): void {
    this.messenger.loading.next(true);
    this.http.get<RequestProgression[]>('tracking/requests').subscribe(requests => {
      this.requests = requests;
      this.messenger.loading.next(false);
    }, () => {
      this.error = 'An error occured while retrieving list of requests.'
      this.messenger.loading.next(false);
    });
  }
}
