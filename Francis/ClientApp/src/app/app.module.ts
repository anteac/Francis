import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule, MatCardModule, MatInputModule, MatListModule, MatProgressSpinnerModule, MatTabsModule } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LogsComponent } from './components/logs/logs.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { OmbiComponent } from './components/ombi/ombi.component';
import { TelegramComponent } from './components/telegram/telegram.component';

@NgModule({
  declarations: [
    AppComponent,

    NavBarComponent,
    HomeComponent,
    TelegramComponent,
    OmbiComponent,
    LogsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'telegram', component: TelegramComponent },
      { path: 'ombi', component: OmbiComponent },
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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
