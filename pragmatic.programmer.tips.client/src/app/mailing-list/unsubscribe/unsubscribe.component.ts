import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

import { CookieService } from 'ngx-cookie-service';

import { MailingListService } from 'src/app/services/mailing-list.service';

@Component({
  selector: 'app-unsubscribe',
  templateUrl: './unsubscribe.component.html',
  styleUrls: ['./unsubscribe.component.scss'],
})
export class UnsubscribeComponent implements OnInit {
  unsubscribeForm: FormGroup = <FormGroup>{};
  resultMessage = '';
  captcha = '';

  constructor(
    private cookieService: CookieService,
    private mailingListService: MailingListService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.unsubscribeForm = this.formBuilder.group({
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  unsubscribeHandler(): void {
    // check for the sub_guard cookie
    if (this.cookieService.check('unsub_guard')) {
      // show fake subscribed message and return
      this.resultMessage =
        'You have been unsubscribed from The Pragmatic Programmer Tips Email Service!';

      // clear the form
      this.clearHandler();
      return;
    }

    let email = this.unsubscribeForm.controls['email'].value;
    this.mailingListService.unsubscribe(email).subscribe((response) => {
      if (response === 'email removed from existingMailingList') {
        this.resultMessage =
          'You have been unsubscribed from The Pragmatic Programmer Tips Email Service.';
      } else if (
        response === 'no email in mailing list matching provided email'
      ) {
        this.resultMessage =
          'No email matching the provided email could be found in the mailing list.';
        return;
      } else {
        this.resultMessage = `An issue has ocurred and you were not unsubscribed. Here is the raw message: [${response}].`;
      }

      //set sub_guard cookie to prevent sub spam
      let timestamp = new Date(Date.now());
      timestamp.setMinutes(timestamp.getMinutes() + 15);
      this.cookieService.set('unsub_guard', 'guarded', timestamp);

      // clear the form
      this.clearHandler();
    });
  }

  clearHandler(): void {
    this.unsubscribeForm.reset({ email: '' });
  }
}
