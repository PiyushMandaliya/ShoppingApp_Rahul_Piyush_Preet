using ShoppingApp.Entities;
using ShoppingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Utility;
using Utility.Authentication;
using Utility.Monads;

namespace ShoppingApp.ViewModel.UserViewModel
{

    //Author: Preet Rajesh Kansara
    public class RegistrationViewModel : ViewModel
    {
        public ObservableCollection<string> SecurityQuestionList { get; }
        private IUserService userService;
        public event Action<string> RegistrationError;
        public event Action LogInAction;
        public DelegateCommand RegistrationCommand { get; }
        public DelegateCommand LogInCommand { get; }



        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                NotifyPropertyChanged(nameof(userName));
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                NotifyPropertyChanged(nameof(firstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                NotifyPropertyChanged(nameof(lastName));
            }
        }


        private string selectedQuestion;
        public string SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                selectedQuestion = value;
                NotifyPropertyChanged(nameof(SelectedQuestion));
            }
        }

        private string answer;
        public string Answer
        {
            get => answer;
            set
            {
                answer = value;
                NotifyPropertyChanged(nameof(answer));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(password));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                NotifyPropertyChanged(nameof(confirmPassword));
            }
        }

        public RegistrationViewModel(IUserService userService)
        {
            this.userService = userService;
            RegistrationCommand = new DelegateCommand(Register);
            LogInCommand = new DelegateCommand(LogIn);
            SecurityQuestionList = getSecurityQuestions();
        }

        private ObservableCollection<string> getSecurityQuestions()
        {
            return new ObservableCollection<string>()
            {
                "What is your Oldest Sibling's Middle Name?",
                "In which Year did you graduate from High School",
                "What is your Nephew's/Niece's Name,",
                "What is the name of the town where you were born?",
                "What was your first car?",
                "Who was your Childhood Favourite SportsPerson?"
            };
        }


        private void Register(object _)
        {
            if (IsRegisterSuccessful())
            {
                User user = new User(userName, firstName, lastName, selectedQuestion, answer, PasswordUtilityCore.GeneratePasswordHash(password));
                Result result = userService.RegisterUser(user);
                if (result.Successful)
                {
                    RegistrationError?.Invoke("Signup Successfully");
                    ClearData();
                }
                else
                    RegistrationError?.Invoke(result.ErrorMessage);
            }
        }


        private void ClearData()
        {
            UserName = null;
            Password = null;
            LastName = null;
            FirstName = null;
            SelectedQuestion = null;
            Answer = null;
            ConfirmPassword = null;
        }

        private void LogIn(object _)
        {
            LogInAction?.Invoke();
        }


        private bool IsRegisterSuccessful()
        {
            return IsValidateUserName() && IsValidateFirstName() && IsValidateLastName()
                && IsValidateSecurityQuestion() && IsValidateSecurityAnswer() && IsValidatePassword();
        }

        private bool IsValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                RegistrationError?.Invoke("Please Enter Valid User Name");
                return false;
            }

            return true;
        }

        private bool IsValidateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                RegistrationError?.Invoke("Please Enter Valid First Name");
                return false;
            }

            return true;
        }

        private bool IsValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                RegistrationError?.Invoke("Please Enter Valid Last Name");
                return false;
            }

            return true;
        }

        private bool IsValidateSecurityQuestion()
        {
            if (string.IsNullOrWhiteSpace(selectedQuestion))
            {
                RegistrationError?.Invoke("Please select Security Question");
                return false;
            }

            return true;
        }

        private bool IsValidateSecurityAnswer()
        {
            if (string.IsNullOrWhiteSpace(Answer))
            {
                RegistrationError?.Invoke("Please Enter Valid Security Answer");
                return false;
            }

            return true;
        }

        private bool IsValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                RegistrationError?.Invoke("Please enter valid password");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                RegistrationError?.Invoke("Please enter valid confirm password");
                return false;
            }
            else if (Password != ConfirmPassword)
            {
                RegistrationError?.Invoke("Confirm Password doesn't match... Please Try Again");
                return false;
            }
            return true;
        }
    }
}
