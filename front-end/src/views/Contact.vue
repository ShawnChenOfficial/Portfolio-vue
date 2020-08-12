<template>
  <div class="Contact">
    <div
      v-bind:class="{
        'toast-notification pl-1 pr-1 text-white text-center': true,
        'toast-notification-show-failed': Failed_Show && Show_Current == 1,
        'toast-notification-hide-failed': !Failed_Show && Show_Current == 1,
        'toast-notification-show-success': Success_Show && Show_Current == 2,
        'toast-notification-hide-success': !Success_Show && Show_Current == 2
      }"
    >
      <h6 class="pt-3">{{ Response.Title }}</h6>
      <p>{{ Response.Message }}</p>
    </div>
    <div id="contact-form">
      <div class="text-center pb-5">
        <h1 class="text-white">Send me an email</h1>
      </div>
      <form
        class="container"
        @submit.prevent="OnSubmit"
        autocomplete="off"
        ref="contactForm"
      >
        <div class="form-row">
          <div class="form-group col-md-4">
            <label>FirstName</label>
            <input
              class="form-control"
              id="FirstName"
              v-model="form.FirstName"
            />
            <span class="text-danger" v-if="!FirstNameIsValid"
              >Please enter your first name</span
            >
          </div>
          <div class="form-group col-md-4">
            <label>LastName</label>
            <input class="form-control" id="LastName" v-model="form.LastName" />
            <span class="text-danger" v-if="!LastNameIsValid"
              >Please enter your last name</span
            >
          </div>
          <div class="form-group col-md-4">
            <label>Gender</label>
            <select class="form-control" id="Gender" v-model="form.Gender">
              <option value="" selected>--Select Gender--</option>
              <option value="Mr.">Mr.</option>
              <option value="Mrs.">Mrs.</option>
              <option value="Miss.">Miss.</option>
            </select>
            <span class="text-danger" v-if="!GenderIsValid"
              >Please select your title</span
            >
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col-md-6">
            <label for="Email">Email</label>
            <input class="form-control" id="Email" v-model="form.Email" />
            <span class="text-danger" v-if="!EmailIsValid"
              >Please a valid email address</span
            >
          </div>
          <div class="form-group col-md-6">
            <label for="Mobile">Mobile</label>
            <input
              class="form-control"
              id="Mobile"
              v-model="form.Mobile"
              placeholder="Optional"
            />
          </div>
        </div>
        <div class="form-row">
          <div class="form-group col-md-12">
            <label for="ExtraComment">ExtraComment</label>
            <textarea
              class="form-control"
              id="ExtraComment"
              v-model="form.ExtraComment"
              rows="3"
              placeholder="Optional"
            ></textarea>
          </div>
        </div>
        <div class="text-center">
          <input
            type="submit"
            class="btn btn-primary"
            value="Submit"
            :disabled="!FormIsValid"
            :hidden="Sending"
          />
          <div :hidden="!Sending" class="btn btn-primary" disabled="disabled">
            <img src="/img/loading.gif" style="max-height: 20px;" />
            Sending...
          </div>
        </div>
      </form>
    </div>
    <div class="text-center" id="second-Page">
      <div class="container vertical-middle pt-v-sm-20vh">
        <h1 class="pb-5 text-white">Or contact with me through</h1>
        <div class="row">
          <div class="col-sm-10 offset-sm-1 offset-md-0 col-md-4 mb-4">
            <div class="card bg-none">
              <span
                class="material-icons card-img-top pt-4 font-88px text-wheat"
                >email</span
              >
              <div class="card-body">
                <h5 class="card-title text-white">Email</h5>
                <a class="card-text" href="mailto:shawnchenofficial@gmail.com"
                  >shawnchenofficial@gmail.com</a
                >
                <a class="card-text" href="mailto:2017003399@student.sit.ac.nz"
                  >2017003399@student.sit.ac.nz</a
                >
              </div>
            </div>
          </div>
          <div class="col-sm-10 offset-sm-1 offset-md-0 col-md-4 mb-4">
            <div class="card bg-none">
              <span
                class="material-icons card-img-top pt-4 font-88px text-wheat"
                >call</span
              >
              <div class="card-body">
                <h5 class="card-title text-white">Mobile</h5>
                <a class="card-text" href="tel:+640224756845"
                  >+64 022 475 6845</a
                >
              </div>
            </div>
          </div>
          <div class="col-sm-10 offset-sm-1 offset-md-0 col-md-4 mb-4">
            <div class="card bg-none">
              <span
                class="material-icons card-img-top pt-4 font-88px text-wheat"
                >location_city</span
              >
              <div class="card-body">
                <h5 class="card-title text-white">Location</h5>
                <p class="card-text">
                  71 Macmaster Street, Richmond, Invercargill, New Zealand
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div id="Top" @click="Top">
      <span class="material-icons">
        arrow_upward
      </span>
      <p class="pl-1 mb-0">
        Top
      </p>
    </div>
    <div @click="Next" id="furtherInfo">
      <p class="mb-0">
        Next
      </p>
      <span class="material-icons">
        arrow_downward
      </span>
    </div>
  </div>
</template>

<script src="https://www.google.com/recaptcha/api.js"></script>
<script>
import $ from "jquery";
import root from "../App.vue";
import Axios from "axios";
import AboutMeVue from "./AboutMe.vue";
import Qs from "qs";
// @ is an alias to /src
const Show_Status = Object.freeze({ Failed: 1, Success: 2, None: 3 });
export default {
  name: "AboutMe",
  data() {
    return {
      timer: "",
      form: {
        FirstName: "",
        LastName: "",
        Gender: "",
        Email: "",
        Mobile: "",
        ExtraComment: ""
      },
      Failed_Show: false,
      Success_Show: false,
      Show_Current: Show_Status.None,
      Response: {
        Title: "",
        Message: ""
      },
      url_email: this.$store.getters.backEnd_baseUrl + "/email/Send",
      Sending: false
    };
  },
  mounted() {
    $(window).unbind("scroll");
    $(window).on("load", root.methods.UpDownArrowsSwitcher());
    $(window).resize(function() {
      root.methods.UpDownArrowsSwitcher();
    });
    $(window).scroll(function() {
      var current = $(document).scrollTop();
      var firstP = $("#contact-form").offset().top;
      if (!root.methods.ToDisableNavigationArrows()) {
        if (current <= firstP + 10) {
          $("#furtherInfo").css({ display: "block" });
        } else {
          $("#furtherInfo").css({ display: "none" });
        }
      }
    });
  },
  computed: {
    FirstNameIsValid() {
      return !this.form.FirstName ? false : true;
    },
    LastNameIsValid() {
      return !this.form.LastName ? false : true;
    },
    GenderIsValid() {
      return !this.form.Gender ? false : true;
    },
    EmailIsValid() {
      if (!this.form.Email) {
        return false;
      }
      var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      if (!re.test(this.form.Email)) {
        return false;
      }
      return true;
    },
    FormIsValid() {
      return (
        this.FirstNameIsValid &&
        this.LastNameIsValid &&
        this.GenderIsValid &&
        this.EmailIsValid
      );
    }
  },
  methods: {
    Next() {
      var current = $(document).scrollTop();
      var firstP = $("#contact-form").offset().top;
      var secondP = $("#second-Page").offset().top;
      if (current <= firstP + 10) {
        $("html, body").animate({ scrollTop: secondP + 100 }, 1000);
      }
    },
    Top() {
      root.methods.Top();
    },
    async OnSubmit() {
      this.Sending = true;
      if (!this.FormIsValid) {
        console.log(this.FormIsValid);
      } else {
        await Axios.post(this.url_email + "?json=" + JSON.stringify(this.form))
          .then(response => {
            if (response.data.toString() == "true") {
              this.Response.Title = "Success!";
              this.Response.Message =
                "Thanks for your email.I will get in touch with you as soon as possible.";
              this.Show_Current = Show_Status.Success;
              this.Success_Show = true;
              this.timer = setTimeout(() => {
                this.Success_Show = false;
              }, 3500);
            } else {
              this.Response.Title = "Ops!";
              this.Response.Message =
                "Email was not successfully sent due to a system error, Please try later or manually send me an email through my email at the bottom of this page.";
              this.Show_Current = Show_Status.Failed;
              this.Failed_Show = true;
              this.timer = setTimeout(() => {
                this.Failed_Show = false;
              }, 3500);
            }
            //clean up the toast notification content
            this.timer = setTimeout(() => {
              this.Response.Title = "";
              this.Response.Message = "";
              this.Show_Current = Show_Status.None;
            }, 4500);
          })
          .catch(error => {
            this.Response.Title = "Ops!";
            this.Response.Message =
              "Email was not successfully sent due to a system error, Please try later or manually send me an email through my email at the bottom of this page.";
            this.Show_Current = Show_Status.Failed;
            this.Failed_Show = true;
            this.timer = setTimeout(() => {
              this.Failed_Show = false;
            }, 3500);
            //clean up the toast notification content
            this.timer = setTimeout(() => {
              this.Response.Title = "";
              this.Response.Message = "";
              this.Show_Current = Show_Status.None;
            }, 4500);
          });
      }
      this.Sending = false;
    }
  },
  beforeDestroy() {
    clearTimeout(this.timer);
  }
};
</script>
