import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './root/app.component';
import { AppRoutingModule } from './routing/app-routing.module';
import { SubscribeComponent } from './mailing-list/subscribe/subscribe.component';
import { UnsubscribeComponent } from './mailing-list/unsubscribe/unsubscribe.component';

@NgModule({
  declarations: [AppComponent, SubscribeComponent, UnsubscribeComponent],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,

    NgbModule,
    FontAwesomeModule,

    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
