<template>
    <div v-if="isVisible"
         class="lil-context-menu"
         tabindex="-1"
         @blur="close"
         @click="close"
         @contextmenu.capture.prevent>
        <slot :menu="userData" ></slot>
    </div>
</template>
<script>
    /* ******************************************************************
    *
    * ae-context-menu
    *
    * in  void
    *
    ******************************************************************* */
    export default {
        name: 'lil-context-menu',
        data() {
            return {
                x: null,
                y: null,
                userData: null
            }
        },
        computed: {
            style() {
                return this.isVisible ? {
                    
                    top: this.y - (document.documentElement.scrollTop || document.body.scrollTop) + 'px',
                    left: this.x + 'px'
                } : {}
            },
            isVisible() {
                return this.x !== null && this.y !== null
            }
        },
        methods: {
            open(evt, userData) {
                this.x = evt.pageX || evt.clientX
                this.y = evt.pageY || evt.clientY
                this.userData = userData
                this.$nextTick(() => this.$el.focus())
            },
            close(evt) {
                this.x = null
                this.y = null
                this.userData = null
            }
        }
    }
</script>

<style scoped>
    .lil-context-menu {
        position: fixed;
        z-index: 999;
    }

    .lil-context-menu:focus {
        outline: none;
    }

    .options {
        min-width: 100px;
        padding-top: 6px;
        padding-right: 10px;
        padding-left: 10px;
        padding-bottom: 6px;
        border: 1px black solid;
        background-color: #fff;
        list-style: none;
        background-clip: padding-box;
        border: 1px solid rgba(0,0,0,.15);
        border-radius: .25rem;
        box-shadow: 0 0 5px #ccc;

    }

    .options li:hover {
        color: #ff0000;
        background-color: whitesmoke;
    }

</style>
