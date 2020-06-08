import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule, MatCardModule, MatChipsModule, MatDialogModule, MatDividerModule, MatIconModule, MatInputModule, MatListModule, MatProgressSpinnerModule, MatSelectModule, MatSidenavModule, MatTabsModule } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LogsComponent } from './components/logs/logs.component';
import { OptionsComponent } from './components/options/options.component';
import { RequestsComponent } from './components/requests/requests.component';
import { UsersComponent } from './components/users/users.component';
import { ShowLogMessageDialog } from './dialogs/show-log-message/show-log-message.dialog';

@NgModule({
  declarations: [
    AppComponent,

    HomeComponent,
    OptionsComponent,
    UsersComponent,
    RequestsComponent,
    LogsComponent,

    ShowLogMessageDialog,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'options', component: OptionsComponent },
      { path: 'users', component: UsersComponent },
      { path: 'requests', component: RequestsComponent },
      { path: 'logs', component: LogsComponent },
      { path: '**', redirectTo: '/' },
    ]),
    BrowserAnimationsModule,

    MatTabsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatSidenavModule,
    MatIconModule,
    MatDividerModule,
    MatSelectModule,
    MatDialogModule,
    MatChipsModule,
  ],
  entryComponents: [
    ShowLogMessageDialog,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
