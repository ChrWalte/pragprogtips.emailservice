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
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.scss'],
})
export class SubscribeComponent implements OnInit {
  subscribeForm: FormGroup = <FormGroup>{};
  resultMessage = '';

  constructor(
    private cookieService: CookieService,
    private formBuilder: FormBuilder,
    private mailingListService: MailingListService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.subscribeForm = this.formBuilder.group({
      email: new FormControl('', [Validators.required, Validators.email]),
      name: new FormControl(''),
    });
  }

  subscribeHandler(): void {
    // check for the sub_guard cookie
    if (this.cookieService.check('sub_guard')) {
      // show fake subscribed message and return
      this.resultMessage =
        'You have been subscribed to The Pragmatic Programmer Tips Email Service!';

      // clear the form
      this.clearHandler();
      return;
    }

    let email = this.subscribeForm.controls['email'].value;
    let name = this.subscribeForm.controls['name'].value || undefined;
    this.mailingListService.subscribe(email, name).subscribe((response) => {
      if (response === 'email added to existingMailingList') {
        this.resultMessage =
          'You have been subscribed to The Pragmatic Programmer Tips Email Service.';
      } else if (
        response === 'existingMailingList already contains provided email'
      ) {
        this.resultMessage =
          'You are already subscribed to The Pragmatic Programmer Tips Email Service.';
        return;
      } else {
        this.resultMessage = `An issue has ocurred and you were not subscribed. Here is the raw message: [${response}].`;
      }

      //set sub_guard cookie to prevent sub spam
      let timestamp = new Date(Date.now());
      timestamp.setMinutes(timestamp.getMinutes() + 15);
      this.cookieService.set('sub_guard', 'guarded', timestamp);

      // clear the form
      this.clearHandler();
    });
  }

  clearHandler(): void {
    this.subscribeForm.reset({
      email: '',
      name: '',
    });
  }
}
