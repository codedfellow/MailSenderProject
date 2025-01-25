import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { FormGroup, Validators, AbstractControlOptions, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedModule } from '../../../shared/shared.module';
import { AuthService } from '../../services/auth.service';
import { AppConfig } from '../../../configurations/app-config';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-register',
    imports: [CommonModule, SharedModule],
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm: FormGroup = this.fb.group({
    userName: ['',[Validators.required,Validators.email]],
    password: ['', [Validators.required]],
    confirmPassword: ['', [Validators.required]]
  },{
    // validators: [ConfirmedPasswordValidator('password','confirmPassword')]
  } as AbstractControlOptions)

  toggleConfirmText: string = 'show'
  toggleText = signal('show')

  authService = inject(AuthService)
  router = inject(Router)
  
  constructor(private fb: FormBuilder) {    
  }
  // constructor(private fb: FormBuilder, private authService: AuthService, private spinner: NgxSpinnerService, private router: Router) {    
  // }

  ngOnInit(): void {
    // throw new Error('Method not implemented.');
  }
  ngOnDestroy(): void {
    // throw new Error('Method not implemented.');
  }

  togglePasswordDisplay(){
    const passwordInput = document.getElementById("password")!
    const passwordToggler = document.getElementById("eye-span")!

    const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);

    passwordInput.getAttribute('type') === 'password' ? this.toggleText.set('show') : this.toggleText.set('hide')
    // passwordToggler.className = ''
    // passwordToggler.className = passwordInput.getAttribute('type') === 'password' ? 'fa-solid fa-eye' : 'fa-regular fa-eye-slash';
  }

  toggleConfirmPasswordDisplay(){
    const passwordInput = document.getElementById("confirmPassword")!
    const passwordToggler = document.getElementById("confirm-eye-span")!

    const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);

    this.toggleConfirmText = passwordInput.getAttribute('type') === 'password' ? 'show' : 'hide'
    // passwordToggler.className = ''
    // passwordToggler.className = passwordInput.getAttribute('type') === 'password' ? 'fa-solid fa-eye' : 'fa-regular fa-eye-slash';
  }
  displayLoading: boolean = false;

  submitRegisterForm(){
    let formVal = this.registerForm.getRawValue()
    
    let body: any = {
      Email: formVal.userName,
      Password: formVal.password,
      ConfirmedPassword: formVal.confirmPassword
    }

    // this.spinner.show();
    this.displayLoading = true;
    
    this.authService.register(body).subscribe({
      next: (res: any) => {
        // this.spinner.hide()
        Swal.fire(AppConfig.AppName,res.message,'success')
        this.displayLoading = false
        this.router.navigate(['auth','login'])
      },
      error: (error: any) => {
        // this.spinner.hide()
        this.displayLoading = false
        console.error('registration error',error)
      }
    })
  }
}
