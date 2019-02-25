<template>
    <b-navbar toggleable="md" variant="faded" type="light">
        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
        <b-collapse is-nav id="nav_collapse">
            <b-navbar-nav>
                <b-nav-item to="/about" :disabled="page==='about'">about</b-nav-item>
                <b-nav-item-dropdown>
                    <template slot="button-content">
                        <em>Example</em>
                    </template>
                    <b-dropdown-item to="/fukuri">Calc</b-dropdown-item>
                    <b-dropdown-item to="/activity">Active</b-dropdown-item>
                    <b-dropdown-item to="/msection">multi section</b-dropdown-item>
                </b-nav-item-dropdown>
                <b-nav-item-dropdown>
                    <template slot="button-content">
                        <em>system</em>
                    </template>
                    <b-dropdown-item to="/table">Table</b-dropdown-item>
                    <b-dropdown-item to="/system">system</b-dropdown-item>
                    <b-dropdown-item to="/account">account</b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>
            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <b-nav-item-dropdown right>
                    <template slot="button-content">
                        <em>{{_lang}}</em>
                    </template>
                    <b-dropdown-item href="#" @click.stop="onChagenLang('ja')">JP</b-dropdown-item>
                    <b-dropdown-item href="#" @click.stop="onChagenLang('en')">EN</b-dropdown-item>
                    <b-dropdown-item href="#" @click.stop="onChagenLang('en-US')">ES</b-dropdown-item>
                    <b-dropdown-item href="#" @click.stop="onChagenLang('ru')">RU</b-dropdown-item>
                    <b-dropdown-item href="#" @click.stop="onChagenLang('fr')">FR</b-dropdown-item>
                </b-nav-item-dropdown>

                <div v-if="_auth">
                    <b-nav-item-dropdown right>
                        <!-- Using button-content slot -->
                        <template slot="button-content">
                            <em>{{_loginUserName}}</em>
                        </template>
                        <b-dropdown-item href="#" @click.stop="onProfile">Profile</b-dropdown-item>
                        <b-dropdown-item href="#" @click.stop="onSignOut">SignOut</b-dropdown-item>
                    </b-nav-item-dropdown>
                </div>
                <div v-else>
                    <b-nav-item v-if="_applying" href="#" right>{{_loginUserName}} (申請中)</b-nav-item>
                    <b-nav-item v-if="!_applying" href="#" @click.stop="onSignIn" right>SignIn</b-nav-item>
                </div>
            </b-navbar-nav>

        </b-collapse>
    </b-navbar>
</template>

<script>
    /* *****************************************************************
    *
    * ae-menu
    *
    * in string  page
    *
    * emit void
    *
    ******************************************************************* */
    import aeTransition from "./ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        props: ["page"],
        data() {
            return {
            }
        },
        methods: {
            /* ******************************************************************
             *
             * onChagenLang
             *
             * param  void
             * return void
             *
             ******************************************************************* */
            onChagenLang: function (lng) {
                localStorage.setItem('lang', lng)
                location.reload();
            },
            /* ******************************************************************
             *
             * onProfile
             *
             * param  void
             * return void
             *
             ******************************************************************* */
            onProfile: function () {
                if (this.$store.state.signInUser.authorized) {
                    var authUserStr = localStorage.getItem('signInUser');
                    //console.log(authUserStr)
                    this.invoke({ page: "auth", section: "edit", action: "edit/load", paramItems: "accountId" }, authUserStr);
                } else {
                    alert("先にSignInしてください")
                    this.invoke({ page: "auth", section: "signIn", action: "signIn" });
                }
            },
            /* ******************************************************************
             *
             * onSignIn
             *
             * param  void
             * return void
             *
             ******************************************************************* */
            onSignIn: function () {
                this.invoke({ page: "auth", section: "signIn", action: "signIn" });
            },
            /* ******************************************************************
             *
             * onSignOut
             *
             * param  void
             * return void
             *
             ******************************************************************* */
            onSignOut: function () {
                localStorage.clear()
                this.$store.dispatch('setSignInUser', {});
                this.invoke({ page: "auth", section: "signIn", action: "signIn"  });
            },

        },
        created: function () {
        },
        mounted: function () {
        },
        computed: {
            _loginUserName: function () {
                return this.$store.state.signInUser.userName || "Anonymous";
            },
            _auth: function () {
                return this.$store.state.signInUser.authorized || false;
            },
            _lang: function () {
                return this.$store.state.lang || 'Lang';
            },
            _applying: function () {
                return this.$store.state.signInUser.applying || false;
            },

        },

    }
</script>
<style>
</style>

<style scoped>
    a.nav-link.disabled {
        color: #007bff !important;
    }
</style>
